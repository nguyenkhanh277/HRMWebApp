using HRMWebApp.Helper;
using System;
using System.Web;

namespace HRMWebApp.Models
{
    public class InfoLogin
    {
        public String Username { get; set; }
        public String CompanyID { get; set; }
        public String EmployeeName { get; set; }
        public DateTime LoginTime { get; set; }
        public string[] Roles { get; set; }

        public static InfoLogin GetCurrentUser(HttpContext context)
        {
            InfoLogin infoLogin = (InfoLogin)context.Session[GlobalConstants.SESSION_KEY_USER];

            return infoLogin;
        }
    }
}