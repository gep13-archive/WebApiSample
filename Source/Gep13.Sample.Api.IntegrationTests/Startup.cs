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

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
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