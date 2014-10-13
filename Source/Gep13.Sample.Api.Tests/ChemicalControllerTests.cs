// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalControllerTests.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalControllerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http.Results;

    using Autofac;

    using AutoMapper;

    using FakeItEasy;

    using Gep13.Sample.Api.Controllers;
    using Gep13.Sample.Api.ViewModels;
    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;
    using Gep13.Sample.Service;

    using NUnit.Framework;

    public class ChemicalControllerTests
    {
        private static Assembly[] assemblies = { Assembly.Load("Gep13.Sample.Api") };
        private static IContainer container;
        private Chemical chemical = new Chemical { Id = 1, Name = "Chemical1", IsArchived = false, Balance = 1 };
        private Service.ChemicalViewModel serviceViewModel = new Service.ChemicalViewModel { Id = 1, Name = "Chemical1", IsArchived = false, Balance = 1 };

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            SetupAutoMapper();
            var containerBuilder = new ContainerBuilder();
            var service = A.Fake<IChemicalService>();
            A.CallTo(() => service.GetChemicalById(1)).Returns(this.serviceViewModel);
            containerBuilder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().AsImplementedInterfaces();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().AsImplementedInterfaces();
            containerBuilder.RegisterType<ChemicalRepository>().As<IChemicalRepository>();
            containerBuilder.RegisterInstance(service);
            container = containerBuilder.Build();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
        }

        [Test]
        public void WhenGetByIdIsCalledUnderlyingServiceIsCalled()
        {
            var service = container.Resolve<IChemicalService>();
            var controller = new ChemicalController(service);
            var actionResult = controller.Get(1);
            var negResult = actionResult as OkNegotiatedContentResult<ViewModels.ChemicalViewModel>;

            Assert.IsNotNull(negResult);

            var chemicalViewModel = negResult.Content;
            Assert.IsNotNull(chemicalViewModel);

            Assert.AreEqual(chemicalViewModel.Id, this.chemical.Id);
        }

        private static void SetupAutoMapper()
        {
            Mapper.Reset();

            Mapper.Initialize(cfg =>
            {
                foreach (var assembly in assemblies)
                {
                    var profiles = assembly.GetTypes()
                        .Where(t => (t.IsSubclassOf(typeof(Profile)) && t.GetConstructor(Type.EmptyTypes) != null))
                        .Select(p => (Profile)Activator.CreateInstance(p));

                    foreach (var item in profiles)
                    {
                        cfg.AddProfile(item);
                    }
                }

                cfg.AllowNullDestinationValues = true;
            });
        }
    }
}