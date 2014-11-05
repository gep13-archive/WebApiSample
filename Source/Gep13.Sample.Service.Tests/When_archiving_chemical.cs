using Gep13.Sample.Model;
using NUnit.Framework;
using Simple.Data;

namespace Gep13.Sample.Service.Test {
    
    [TestFixture]
    public class When_archiving_chemical 
    {

        private ChemicalService _chemicalService;


        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
        }

        [SetUp]
        public void Setup() 
        {
            _chemicalService = new ChemicalService();            
        }

        [Test]
        public void Should_return_false_if_unable_to_find_chemical()
        {
            var actual = _chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Should_return_true_if_archives_chemical() {
            var entity = new Chemical 
            {
                Id = 1
            };

            var db = Database.Open();
            db.Chemicals.Insert(entity);

            var actual = _chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.True);          

        }
    }
}