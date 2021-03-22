using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMWebApp.Models
{
    public class ServerMessage
    {
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string HowToFix { get; set; }
        public string BackPage { get; set; }
    }
}