using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace bangw8
{
    class Program
    {
        /// <summary>
        /// 网页文件名
        /// </summary>
        private static readonly string htmlFileName = "BangWo8Client.html";

        /// <summary>
        /// 生成网页的文件名，含路径
        /// </summary>
        private static string runTimeHtmlFileName = ""; 

        /// <summary>
        /// 生成网页的路径
        /// </summary>
        private static string basePath = "";

        /// <summary>
        /// 金蝶用户ID
        /// </summary>
        private static string authAccount = "";
        
        /// <summary>
        /// 通讯密码
        /// </summary>
        private static readonly string private_Key = "2300bed5297512b56c67c1433c523f83";

        /// <summary>
        ///  正通的帮我吧ID
        /// </summary>
        private static readonly string vendorID = "291622";

        /// <summary>
        /// 当前时间戳
        /// </summary>
        private static string timestamp = "";

        /// <summary>
        /// 加密信息
        /// </summary>
        private static string signature = "";

        /// <summary>
        /// 随机数
        /// </summary>
        private static string nonce = "";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            String[] CmdArgs = System.Environment.GetCommandLineArgs();

            if (CmdArgs[1] != null)
            {
                basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
                runTimeHtmlFileName = @basePath + htmlFileName;

                authAccount = CmdArgs[1].ToString();
                timestamp = GetTimeStamp();
                nonce =  GetNonce();
                signature = GetSignature();

                ////读取1.html
                byte[] array = Encoding.ASCII.GetBytes(BuildTemplateHtml1(vendorID, authAccount, timestamp, nonce, signature));
                MemoryStream stream = new MemoryStream(array);

                StreamReader srReadFile = new StreamReader(stream);
                StreamWriter swWriteFile = File.CreateText(runTimeHtmlFileName);

                //// 读取流直至文件末尾结束
                while (!srReadFile.EndOfStream)
                {
                    string strReadLine = srReadFile.ReadLine(); //读取每行数据
                    //if (strReadLine.IndexOf("123456789") > -1)
                    //{
                    //    ////替换value_field
                    //    strReadLine = strReadLine.Replace("123456789", authAccount);
                    //    strReadLine = strReadLine.Replace("authAccount", GetTimeStampMicroSecond().ToString());
                    //    strReadLine = strReadLine.Replace("ccbbaa", GetTimeStampMicroSecond().ToString());
                    //}
                    swWriteFile.WriteLine(strReadLine); //写入读取的每行数据
                }

                //// 关闭读取流文件
                srReadFile.Close();
                swWriteFile.Close();
                ///
                OpenBrowserUrl(runTimeHtmlFileName);
                //return;
            }
        }

        /// <summary>
        /// 调用系统浏览器打开网页
        /// </summary>
        /// <param name="url">打开网页的链接</param>
        public static void OpenBrowserUrl(string url)
        {
            try
            {
                // 64位注册表路径
                var openKey = @"SOFTWARE\Wow6432Node\Google\Chrome";
                if (IntPtr.Size == 4)
                {
                    // 32位注册表路径
                    openKey = @"SOFTWARE\Google\Chrome";
                }
                RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
                // 谷歌浏览器就用谷歌打开，没找到就用系统默认的浏览器
                // 谷歌卸载了，注册表还没有清空，程序会返回一个"系统找不到指定的文件。"的bug
                if (appPath != null)
                {
                    var result = Process.Start("chrome.exe", "--app=" + url + " --start-maximized");
                }
                else
                {
                    var result = Process.Start("chrome.exe", "--app=" + url + " --start-maximized");
                }
            }
            catch
            {
                // 出错调用用户默认设置的浏览器，还不行就调用IE
                //OpenDefaultBrowserUrl(url);
            }
        }
        
        /// <summary>  
        /// 获取时间戳  13位
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000).ToString();
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <returns></returns>
        public static string GetSignature()
        {
            //获取参数
            string[] Para = new string[] { vendorID, authAccount, private_Key, timestamp, nonce };
            //排序
            Array.Sort(Para);
            //转成文本
            string strPara = string.Join("",Para);    
            //SHA1（参数）
            return Ryan.Helper.Crypto.ShaxEncrypt.Sha1Encrypt(strPara);
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <returns></returns>
        private static string GetNonce()
        {
            return "";
        }

        /// <summary>
        /// 当前时间戳（秒）
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStampSecond()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return ((int)(DateTime.UtcNow - startTime).TotalSeconds).ToString();
        }

        /// <summary>
        /// 生成网页模板（旧）
        /// </summary>
        /// <returns></returns>
        private static string BuildTemplateHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html>\n");
            sb.Append("<html lang = \"en\">\n");
            sb.Append("<head>\n");
            sb.Append("<Title>BangWo8 Client V3.0</Title>\n");
            sb.Append("<meta http-equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" />\n");
            sb.Append("<meta charset = \"utf -8\" />\n");
            sb.Append("<meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />\n");
            sb.Append("</head>\n");
            sb.Append("<body>\n");
            sb.Append("<span id = \"bw8-third-party\" data-thirdparty = '{\"field_key\":\"authAccount\",\"field_value\":\"123456789\",\"tagSkillList\":\"\",\"permissions\":1}' ></span>\n");
            sb.Append("<script language=\"javascript\" type=\"text/javascript\"  charset=\"gb2312\" src=\"https://www.bangwo8.com/osp2016/chat/js/chatInit_v3.js?bangwo8Info=OTgwMTI5MTYyMmE2N2YwOTY4MDk0MTVjYTFjOWYxMTJkOTZkMjc2ODli\" async=\"async\" id=\"bangwo8Info\"  vn=\"1709\" custominfo=\"\" tagName=\"\"></script>\n");
            sb.Append("</body>\n");
            sb.Append("</html>\n");
            return sb.ToString();
        }


        /// <summary>
        /// 生成网页模板
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="uid"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        private static string BuildTemplateHtml1(string vendorID, string uid,string timestamp,string nonce,string signature)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html>\n");
            sb.Append("<html lang = \"en\">\n");
            sb.Append("<head>\n");
            sb.Append("<Title>BangWo8 Client V3.0</Title>\n");
            sb.Append("<meta http-equiv = \"Content-Type\" content = \"text/html; charset=utf-8\" />\n");
            sb.Append("<meta charset = \"utf-8\" />\n");
            sb.Append("<meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />\n");
            sb.Append("</head>\n");
            sb.Append("<body>\n");
            sb.Append("<a href='https://www.bangwo8.com/osp2016/chat/pc/index.php?vendorID={0}&uid={1}&timestamp={2}&nonce={3}&signature={4}'>BangWo8</a>\n");
            sb.Append("</body>\n");
            sb.Append("</html>\n");
            return string.Format(sb.ToString(),vendorID, uid,timestamp,nonce,signature);
        }
    }
}