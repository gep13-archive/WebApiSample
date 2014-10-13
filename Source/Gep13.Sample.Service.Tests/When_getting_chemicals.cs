using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gep13.Sample.Data.Infrastructure;
using Gep13.Sample.Data.Repositories;
using Gep13.Sample.Model;
using NSubstitute;
using NUnit.Framework;

namespace Gep13.Sample.Service.Test {
    [TestFixture]
    public class When_getting_chemicals {

        private IChemicalRepository _fakeRepository;
        private IUnitOfWork _fakeUnitOfWork;
        private ChemicalService _chemicalService;
        private List<Chemical> _checmicals;

        [TestFixtureSetUp]
        public void TestFixtureSetup() {
            Mapper.CreateMap<Chemical, ChemicalViewModel>();
        }

        [SetUp]
        public void SetUp()
        {
            _fakeRepository = Substitute.For<IChemicalRepository>();
            _fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            _chemicalService = new ChemicalService(_fakeRepository, _fakeUnitOfWork);
            _checmicals = new List<Chemical> {
                {new Chemical{Id=1, Name = "First"}},
                {new Chemical{Id=2, Name = "Second"}},
                {new Chemical{Id=3, Name = "Third"}}
            };
        }

        [Test]
        public void Should_get_chemical_by_id()
        {

            //A.CallTo(() => _fakeRepository.GetById(1)).Returns(new Chemical { Id = 1 });
            _fakeRepository.GetById(1).Returns(new Chemical { Id = 1 });

            var checmical = _chemicalService.GetChemicalById(1);

            Assert.That(checmical.Id, Is.EqualTo(1));
            //A.CallTo(()=> _fakeRepository.GetById(1)).MustHaveHappened();
            _fakeRepository.Received().GetById(1);
        }

        [Test]
        public void Shoud_get_chemicals() {
            
            _fakeRepository.GetAll().Returns(_checmicals);

            var actual = _chemicalService.GetChemicals();

            Assert.That(actual.Count(),Is.EqualTo(3));

        }

        [Test]
        public void Should_get_chemical_by_name() {

            const string chemicalName = "First";

            // Don't like this! First time I've found a problem with NSubstitute trying to mock the Expression<func<Checmical,bool>>
            _fakeRepository.GetMany(x => x.Name == Arg.Is(chemicalName)).ReturnsForAnyArgs(_checmicals.Where(x => x.Name == chemicalName));

            var result = _chemicalService.GetChemicalByName(chemicalName).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(chemicalName));
        }

        [Test]
        public void Should_return_empty_IEnumerable() {

            const string missing = "Missing";

            _fakeRepository.GetMany(x => x.Name == Arg.Is(missing)).ReturnsForAnyArgs(_checmicals.Where(x => x.Name == missing));

            var result = _chemicalService.GetChemicalByName(missing);

            Assert.That(result, Is.Empty);
        }

    }
}