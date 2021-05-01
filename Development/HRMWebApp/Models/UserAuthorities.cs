using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMWebApp.Models
{
    public class UserAuthorities
    {
        public String AuthorityGroupId { get; set; }
        public String AuthorityGroupName { get; set; }
        public bool Check { get; set; }
    }
}