using System;
using System.Security.Cryptography;
using System.Text;

namespace Ryan.Helper.Crypto
{
    /// <summary>
    /// SHA【1|256|384|512】 系列加密助手
    /// </summary>
    public sealed class ShaxEncrypt
    {
        /// <summary>
        /// SHA1
        /// </summary>
        /// <param name="palinData">明文</param>
        /// <param name="encodingType">编码方式</param>
        /// <returns>string：密文</returns>
        public static string Sha1Encrypt(string palinData, EncodingStrOrByte.EncodingType encodingType = EncodingStrOrByte.EncodingType.UTF8)
        {
            if (string.IsNullOrWhiteSpace(palinData)) return null;
            using (SHA1 sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] bytes = EncodingStrOrByte.GetBytes(palinData, encodingType);
                byte[] sha1Bytes = sha1.ComputeHash(bytes);
                return Convert.ToBase64String(sha1Bytes);
            }
        }

        /// <summary>
                /// SHA256
                /// </summary>
                /// <param name="palinData">明文</param>
                /// <param name="encodingType">编码方式</param>
                /// <returns>string：密文</returns>
        public static string Sha256Encrypt(string palinData, EncodingStrOrByte.EncodingType encodingType = EncodingStrOrByte.EncodingType.UTF8)
        {
            if (string.IsNullOrWhiteSpace(palinData)) return null;
            using (SHA256 sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] bytes = EncodingStrOrByte.GetBytes(palinData, encodingType);
                byte[] sha256Bytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(sha256Bytes);
            }
        }

        /// <summary>
                /// SHA384
                /// </summary>
                /// <param name="palinData">明文</param>
                /// <param name="encodingType">编码方式</param>
                /// <returns>string：密文</returns>
        public static string Sha384Encrypt(string palinData, EncodingStrOrByte.EncodingType encodingType = EncodingStrOrByte.EncodingType.UTF8)
        {
            if (string.IsNullOrWhiteSpace(palinData)) return null;
            using (SHA384 sha384 = new SHA384CryptoServiceProvider())
            {
                byte[] bytes = EncodingStrOrByte.GetBytes(palinData, encodingType);
                byte[] sha384Bytes = sha384.ComputeHash(bytes);
                return Convert.ToBase64String(sha384Bytes);
            }
        }

        /// <summary>
                /// SHA512
                /// </summary>
                /// <param name="palinData">明文</param>
                /// <param name="encodingType">编码方式</param>
                /// <returns>string：密文</returns>
        public static string Sha512Encrypt(string palinData, EncodingStrOrByte.EncodingType encodingType = EncodingStrOrByte.EncodingType.UTF8)
        {
            if (string.IsNullOrWhiteSpace(palinData)) return null;
            using (SHA512 sha512 = new SHA512CryptoServiceProvider())
            {
                byte[] bytes = EncodingStrOrByte.GetBytes(palinData, encodingType);
                byte[] sha512Bytes = sha512.ComputeHash(bytes);
                return Convert.ToBase64String(sha512Bytes);
            }
        }


        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <returns>返回40位UTF8 大写</returns>
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                string result = BitConverter.ToString(bytes_out);
                //result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
    }
}