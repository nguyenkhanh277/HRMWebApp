using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace HRMWebApp.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityHelper
    {
        public static String CreateSalt(int size)
        {
            if (size <= 0) return "";
            int gSize = size + 5;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff).Substring(0, size);
        }

        public static String GenerateMD5(String input, String salt)
        {
            String inputValue = String.Format("{1}_{0}*{1}", input, salt);
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(inputValue)).Select(s => s.ToString("x2")));
        }

        public static String nl2br(string InputString)
        {
            return InputString.Replace("\n", "<br />");
        }

        public static String br2nl(string InputString)
        {
            return InputString.Replace("<br />", "\n");
        }
    }
}