// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the WebApiApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Gep13.Sample.Api.AppStart;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected static void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bootstrapper.Configure();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}