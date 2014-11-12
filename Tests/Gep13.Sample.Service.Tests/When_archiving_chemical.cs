using Gep13.Sample.Data.Infrastructure;
using Gep13.Sample.Data.Repositories;
using Gep13.Sample.Model;
using NSubstitute;
using NUnit.Framework;

namespace Gep13.Sample.Service.Test {
    
    [TestFixture]
    public class When_archiving_chemical 
    {
        private IChemicalRepository _fakeRepository;
        private IUnitOfWork _fakeUnitOfWork;
        private ChemicalService _chemicalService;


        [SetUp]
        public void Setup() 
        {
            _fakeRepository = Substitute.For<IChemicalRepository>();
            _fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            _chemicalService = new ChemicalService(_fakeRepository, _fakeUnitOfWork);            
        }

        [Test]
        public void Should_return_false_if_unable_to_find_chemical() 
        {
            _fakeRepository.GetById(1).Returns(x => null);

            var actual = _chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Should_return_true_if_archives_chemical() {
            var entity = new Chemical 
            {
                Id = 1
            };

            _fakeRepository.GetById(1).Returns(x => entity);

            var actual = _chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.True);
            Assert.That(entity.IsArchived, Is.True);
            

        }
    }
}