using Gep13.Sample.Data.Infrastructure;
using Gep13.Sample.Data.Repositories;
using Gep13.Sample.Model;
using NSubstitute;
using NUnit.Framework;

namespace Gep13.Sample.Service.Test {
    [TestFixture]
    public class When_deleting_chemicals {
        
        private IChemicalRepository _fakeRepository;
        private IUnitOfWork _fakeUnitOfWork;
        private ChemicalService _chemicalService;


        [SetUp]
        public void SetUp()
        {
            _fakeRepository = Substitute.For<IChemicalRepository>();
            _fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            _chemicalService = new ChemicalService(_fakeRepository, _fakeUnitOfWork);
        }

        [Test]
        public void Should_delete_chemical()
        {

            var fakeChemical = new Chemical { Id = 1 };

            _fakeRepository.GetById(Arg.Any<int>()).Returns(fakeChemical);

            _chemicalService.DeleteChemical(1);

            _fakeRepository.Received().Delete(fakeChemical);
            _fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_not_delete_chemical_if_not_found()
        {
            _fakeRepository.GetById(Arg.Any<int>()).Returns(r => null);

            _chemicalService.DeleteChemical(1);

            _fakeRepository.DidNotReceive().Delete(Arg.Any<Chemical>());
            _fakeUnitOfWork.DidNotReceive().SaveChanges();
        }         
    }
}