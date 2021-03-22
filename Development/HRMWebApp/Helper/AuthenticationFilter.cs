using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace HRMWebApp.Helper
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserAuthenticationFilter : ActionFilterAttribute, IActionFilter
    {
        private bool isAjaxRequest;
        private bool allUser;
        public UserAuthenticationFilter(bool ajaxRequest = false, bool AllUser = false)
        {
            isAjaxRequest = ajaxRequest;
            allUser = AllUser;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (
                filterContext.HttpContext.Session[GlobalConstants.SESSION_KEY_USER] == null
       
            )
            {
                
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Home" },
                            { "action", "Login" }
                        });

            }
            else
            {
                bool bAllow = true;

                if (bAllow)
                {
                    this.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "NotAuthentication" }
                    });
                }
                
            }
        }
    }
}