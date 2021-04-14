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
        #region Mã hóa 2 chiều
        /// <summary>
        /// Mã hóa ký tự với kiểu mã hõa TripleDes - MD5
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5_Encrypt(string value)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(value);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes("KSOFT"));
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// Giải mã dữ liệu đã mã hóa
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5_Decrypt(string value)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(value);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes("KSOFT"));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region Mã hóa 1 chiều (Áp dụng cho password)
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
        #endregion
    }
}