// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.IntegrationTests
{
    using System.Reflection;
    using System.Web.Http;

    using Autofac.Integration.WebApi;

    using Gep13.Sample.Common;

    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);

            var config = new HttpConfiguration();
            config.EnableCors();

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //// config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            
            var containerBuilder = AutofacBootstrapper.Configure();

            containerBuilder.RegisterApiControllers(Assembly.Load("Gep13.Sample.Api"));
            
            var container = containerBuilder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);

            app.UseWebApi(config);
        }
    }
}