// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_updating_chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_updating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Test
{
    using System.Reflection;

    using AutoMapper;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_chemical
    {
        private static Assembly[] assemblies = { Assembly.Load("Gep13.Sample.Service") };

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Mapper.CreateMap<ChemicalDto, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDto>();
        }

        [Test]
        public void Should_update()
        {
            var fakeRepository = Substitute.For<IChemicalRepository>();
            var fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            var chemicalService = new ChemicalService(fakeRepository, fakeUnitOfWork);

            var toUpdate = new Service.ChemicalDto
            {
                Id = 1,
                Balance = 110.99,
                Name = "First"
            };

            var actual = chemicalService.UpdateChemical(toUpdate);

            fakeRepository.Received().Update(Arg.Any<Chemical>());
            fakeUnitOfWork.Received().SaveChanges();
        }
    }
}