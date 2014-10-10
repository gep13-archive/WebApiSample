using Gep13.Sample.Data.Infrastructure;
using Gep13.Sample.Data.Repositories;
using Gep13.Sample.Model;
using NSubstitute;
using NUnit.Framework;

namespace Gep13.Sample.Service.Test
{
    [TestFixture]
    public class When_creating_chemical
    {
        private IChemicalRepository _fakeRepository;
        private IUnitOfWork _fakeUnitOfWork;
        private ChemicalService _chemicalService;


        [SetUp]
        public void SetUp() {
            _fakeRepository =  Substitute.For<IChemicalRepository>();
            _fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            _chemicalService = new ChemicalService(_fakeRepository, _fakeUnitOfWork);
        }

        [Test]
        public void Should_get_chemical() {
            
            //A.CallTo(() => _fakeRepository.GetById(1)).Returns(new Chemical { Id = 1 });
            _fakeRepository.GetById(1).Returns(new Chemical { Id = 1 });

            var checmical = _chemicalService.GetChemicalById(1);

            Assert.That(checmical.Id, Is.EqualTo(1));
            //A.CallTo(()=> _fakeRepository.GetById(1)).MustHaveHappened();
            _fakeRepository.Received().GetById(1);
        }

        [Test]
        public void Should_create_chemical() {

            var toAdd = new Chemical {
                Balance = 110.99,
                Name = "First"
            };

            //A.CallTo(() => _fakeRepository.Add(???)))
            _fakeRepository.Add(Arg.Do<Chemical>(x => x.Id =1)).Returns(toAdd);
            _fakeUnitOfWork.SaveChanges();

            var chemical = _chemicalService.AddChemical(toAdd);

            Assert.That(chemical.Id, Is.EqualTo(1));

        }
    }
}
