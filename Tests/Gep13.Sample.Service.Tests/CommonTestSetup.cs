
namespace Gep13.Sample.Service.Tests
{
    using Gep13.Sample.Api.Mappers;
    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class CommonTestSetup
    {
        protected IChemicalRepository fakeChemicalRepository;
        protected IUnitOfWork fakeUnitOfWork;
        protected ChemicalService chemicalService;

        protected CommonTestSetup()
        {

        }

        [TestFixtureSetUp]
        public void InitializeTests()
        {
            AutoMapperConfiguration.Configure();
        }

        [SetUp]
        public void Setup()
        {
            fakeChemicalRepository = Substitute.For<IChemicalRepository>();
            fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            chemicalService = new ChemicalService(fakeChemicalRepository);
        }
    }
}