/*
 * Copyright (c) 2020 ZP
 * Revision: 0.0.0.1
 * CLR: 4.0.30319.42000
 * Date 8/10/2020 5:23:41 PM
 * Name SecurityHelper
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Comlib
{
    /// <summary>
    ///
    /// </summary>
    public class SecurityHelper
    {
        /// <summary>
        /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="bit">位数，一般取值16 或 32</param>
        /// <returns>返回的加密后的字符串</returns>
        public static string MD5Encrypt(string str, int bit = 32)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (var i in hashedDataBytes)
            {
                sb.Append(i.ToString("x2"));
            }
            if (bit == 16)
            {
                return sb.ToString().Substring(8, 16).ToLower();
            }
            else
            {
                return sb.ToString().ToLower();
            }
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
        }

        public static bool IsSafeSqlParam(string value)
        {
            return !Regex.IsMatch(value, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
    }
}