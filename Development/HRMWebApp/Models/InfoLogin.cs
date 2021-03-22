using HRMWebApp.Helper;
using System;
using System.Web;

namespace HRMWebApp.Models
{
    public class InfoLogin
    {
        public String UserName { get; set; }
        public String FullName { get; set; }
        public DateTime LoginTime { get; set; }

        public static InfoLogin GetCurrentUser(HttpContext context)
        {
            InfoLogin infoLogin = (InfoLogin)context.Session[GlobalConstants.SESSION_KEY_USER];

            return infoLogin;
        }
    }
}