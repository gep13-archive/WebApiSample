namespace Gep13.Sample.Api.IntegrationTests.Chemicals
{
    using System.Net;
    using System.Threading.Tasks;

    using Gep13.Sample.Api.ViewModels;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_chemicals : CommonAuthenticatedTestSetup
    {
        private string uriBase = "api/Chemical";
        private string uri = string.Empty;

        protected override string Uri
        {
            get
            {
                return uri;
            }
        }

        [Test]
        public async Task Should_return_statuscode_ok_when_updating_chemical_by_id()
        {
            // Arrange
            this.uri = string.Format("{0}{1}", this.uriBase, "/1");

            // Get the object that we want to update
            var response = await this.GetAsync();
            var chemical = JsonConvert.DeserializeObject<ChemicalViewModel>(response.Content.ReadAsStringAsync().Result);
            chemical.Balance = 99;

            this.uri = this.uriBase;

            // Act
            response = await PutAsync(chemical);
            var jsonContent = JsonConvert.DeserializeObject<ChemicalViewModel>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(99, jsonContent.Balance);
        }

        [Test]
        public async Task Should_return_statuscode_conflict_when_updating_chemical_by_id_and_chemical_name_already_exists()
        {
            // Arrange
            this.uri = this.uriBase;

            var model = new ChemicalViewModel
            {
                Id = 1,
                Name = "Chemical2",
                Code = "1234",
                IsArchived = false,
                Balance = 14,
                RowVersion = "AAAAAAAAAAA="
            };

            // Act
            var response = await PutAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Test]
        public async Task Should_return_statuscode_conflict_when_updating_chemical_by_id_and_chemical_code_already_exists()
        {
            // Arrange
            this.uri = this.uriBase;

            var model = new ChemicalViewModel
            {
                Id = 1,
                Name = "Chemical1",
                Code = "2345",
                IsArchived = false,
                Balance = 14,
                RowVersion = "AAAAAAAAAAA="
            };

            // Act
            var response = await PutAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Test]
        public async Task Should_return_statuscode_preconditionfailed_when_concurrency_exception_occurs()
        {
            // Arrange
            this.uri = this.uriBase;
            
            var model = new ChemicalViewModel
            {
                Id = 1,
                Name = "Chemical1",
                Code = "1234",
                IsArchived = false,
                Balance = 14,
                RowVersion = "AAAAAAAAAAA="
            };

            // Act
            var response = await PutAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.StatusCode);
        }
    }
}