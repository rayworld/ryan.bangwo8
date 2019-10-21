using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace bangw8
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] CmdArgs = System.Environment.GetCommandLineArgs();
            
            string BasePath = "";
            string runTimeName = "";
            string runTimeFile = "";
            string authAccount = "";
            if (CmdArgs[1] != null)
            {
                //BasePath = CmdArgs[0].ToString().Replace("bangw8.exe", "");
                BasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";

                authAccount = CmdArgs[1].ToString();
                runTimeName = string.Format("BangWo8Client.html");
                runTimeFile = @BasePath + runTimeName;

                ////读取1.html
                byte[] array = Encoding.ASCII.GetBytes(BuildTemplateHtml());
                MemoryStream stream = new MemoryStream(array);

                StreamReader srReadFile = new StreamReader(stream);
                StreamWriter swWriteFile = File.CreateText(runTimeFile);

                //// 读取流直至文件末尾结束
                while (!srReadFile.EndOfStream)
                {
                    string strReadLine = srReadFile.ReadLine(); //读取每行数据
                    if (strReadLine.IndexOf("123456789") > -1)
                    {
                        ////替换value_field
                        strReadLine = strReadLine.Replace("123456789", authAccount);
                    }
                    swWriteFile.WriteLine(strReadLine); //写入读取的每行数据
                }

                //// 关闭读取流文件
                srReadFile.Close();
                swWriteFile.Close();
                ///
                OpenBrowserUrl(runTimeFile);
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
    }
}