namespace Gep13.Sample.Api.IntegrationTests.Chemicals
{
    using System.Net;
    using System.Threading.Tasks;

    using Gep13.Sample.Api.ViewModels;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_checmicals : CommonAuthenticatedTestSetup
    {
        protected override string Uri
        {
            get { return "/api/Chemical"; }
        }

        [Test]
        public async Task Should_return_statuscode_ok_when_updating_chemical_by_id()
        {
            // Arrange
            var model = new ChemicalViewModel
            {
                Id = 1,
                Name = "Chemical1",
                Code = "1234",
                IsArchived = false,
                Balance = 14
            };

            // Act
            var response = await PutAsync(model);
            var jsonContent = JsonConvert.DeserializeObject<ChemicalViewModel>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(14, jsonContent.Balance);
        }

        [Test]
        public async Task Should_return_statuscode_conflict_when_updating_chemical_by_id_and_chemical_name_already_exists()
        {
            // Arrange
            var model = new ChemicalViewModel
            {
                Id = 1,
                Name = "Chemical2",
                Code = "1234",
                IsArchived = false,
                Balance = 14
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
            var model = new ChemicalViewModel
            {
                Id = 1,
                Name = "Chemical1",
                Code = "2345",
                IsArchived = false,
                Balance = 14
            };

            // Act
            var response = await PutAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }
    }
}