using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HRMWebApp.Helper
{
    public static class GlobalHelper
    {
        public static bool ValidateExtensionEXCEL(String sExtension)
        {
            return GlobalConstants.FILE_UPLOAD_ALLOW_EXTENSION.Contains(sExtension);
        }
        /// <summary>
        /// Generate a new string include lowercase letter, uppercase letter and number
        /// </summary>
        /// <param name="length">Length of result string</param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
            string sResult = "";
            Random ran = new Random();
            for (int i = 0; i < length; i++)
            {
                sResult += chars[ran.Next(chars.Length)];
            }
            //return new string(Enumerable.Repeat(chars, length)
            //  .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
            return sResult;
        }

        public static string CycleClass(int total, int current, List<String> classList)
        {
            return classList[current % 2].ToString();
        }

        public static string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }

        public static string StringLimit(string src, int strLen)
        {

            if (src.Length <= (strLen - 3)) return src;
            else return src.Substring(0, strLen - 1) + "...";
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// Compact a number to string
        /// </summary>
        /// <param name="value">Number to compact</param>
        /// <returns>Return a string is short number. Example: 1,250 -> Over 1K, 1,323,345 -> Over 1M, 3,234,654,245 -> Over 3B</returns>
        public static string ZipNumber(int value, string OverText)
        {
            string sResult = value.ToString();
            string[] shortUnit = new string[] { "", "K", "M", "B" };
            int i = 0;
            while (sResult.Length > 3)
            {
                sResult = sResult.Substring(0, sResult.Length - 3);
                i += 1;
            }

            return string.Format("{0}{1}{2}", (i > 0 ? OverText : ""), sResult, shortUnit[i]);
        }

        /// <summary>
        /// Remove all embed scripts in HtmlContent
        /// </summary>
        /// <param name="HtmlContent">Html string to remove scripts</param>
        /// <returns>String non scripts</returns>
        public static string RemoveAllHtmlScript(string HtmlContent)
        {
            Regex regScript = new Regex("<script[^>]*?.*?</script>", RegexOptions.IgnoreCase);
            return regScript.Replace(HtmlContent, "");

        }
    }
}