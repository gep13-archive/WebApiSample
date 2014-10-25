// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutofacBootstrapper.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the AutofacBootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Common
{
    using Autofac;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Service;

    public static class AutofacBootstrapper
    {
        public static ContainerBuilder Configure()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<ChemicalService>().As<IChemicalService>().InstancePerRequest();
            containerBuilder.RegisterType<ChemicalRepository>().As<IChemicalRepository>().InstancePerRequest();

            return containerBuilder;
        }
    }
}