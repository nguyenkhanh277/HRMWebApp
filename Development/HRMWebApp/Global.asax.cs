using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;

namespace HRMWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;

            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            json.Culture = new System.Globalization.CultureInfo("en-us");
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'";

            //Force JSON responses on all requests
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(formatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        }
        

    }
}
