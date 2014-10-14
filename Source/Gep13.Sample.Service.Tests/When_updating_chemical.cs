using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Gep13.Sample.Data.Infrastructure;
using Gep13.Sample.Data.Repositories;
using Gep13.Sample.Model;
using NSubstitute;
using NUnit.Framework;

namespace Gep13.Sample.Service.Test {
    [TestFixture]
    public class When_updating_chemical {
        private static Assembly[] assemblies = { Assembly.Load("Gep13.Sample.Service") };

        [TestFixtureSetUp]
        public void TestFixtureSetup() {
            Mapper.CreateMap<ChemicalDTO, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDTO>();
        }

        [Test]
        public void Should_update() {
            var fakeRepository = Substitute.For<IChemicalRepository>();
            var fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            var chemicalService = new ChemicalService(fakeRepository, fakeUnitOfWork);
           
            var toUpdate = new Service.ChemicalDTO {
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