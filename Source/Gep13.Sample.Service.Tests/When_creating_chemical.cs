using Gep13.Sample.Data.Infrastructure;
using Gep13.Sample.Data.Repositories;
using Gep13.Sample.Model;
using NSubstitute;
using NUnit.Framework;

namespace Gep13.Sample.Service.Test {
    [TestFixture]
    public class When_creating_chemical {

        [Test]
        public void Should_create_chemical() {

            var fakeRepository = Substitute.For<IChemicalRepository>();
            var fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            var chemicalService = new ChemicalService(fakeRepository, fakeUnitOfWork);

            var toAdd = new Chemical {
                Balance = 110.99,
                Name = "First"
            };

            //A.CallTo(() => _fakeRepository.Add(???)))
            fakeRepository.Add(Arg.Do<Chemical>(x => x.Id = 1)).Returns(toAdd);


            var chemical = chemicalService.AddChemical(toAdd);

            Assert.That(chemical.Id, Is.EqualTo(1));
            fakeUnitOfWork.Received().SaveChanges();
        }
    }
}