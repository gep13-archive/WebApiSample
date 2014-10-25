// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the Bootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.AppStart
{
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Gep13.Sample.Api.Mappers;
    using Gep13.Sample.Common;

    public static class Bootstrapper
    {
        public static void Configure()
        {
            ConfigureAutofacContainer();
            AutoMapperConfiguration.Configure();
        }

        private static void ConfigureAutofacContainer()
        {
            var webApiContainerBuilder = AutofacBootstrapper.Configure();
            ConfigureWebApiContainer(webApiContainerBuilder);
        }

        private static void ConfigureWebApiContainer(ContainerBuilder containerBuilder)
        {            
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}