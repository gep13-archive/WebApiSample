namespace Gep13.Sample.Api.IntegrationTests.Chemicals
{
    using System.Net;
    using System.Threading.Tasks;

    using Gep13.Sample.Api.ViewModels;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class When_creating_chemicals : CommonAuthenticatedTestSetup
    {
        protected override string Uri
        {
            get { return "/api/Chemical"; }
        }

        [Test]
        public async Task Should_return_statuscode_created_and_new_chemical_if_successful()
        {
            // Arrange
            var model = new ChemicalViewModel
            {
                Name = "Chemical3",
                Code = "3456",
                IsArchived = false,
                Balance = 15
            };

            // Act
            var response = await PostAsync(model);
            var jsonContent = JsonConvert.DeserializeObject<ChemicalViewModel>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(3, jsonContent.Id);
            Assert.AreEqual("Chemical3", jsonContent.Name);
            Assert.AreEqual(false, jsonContent.IsArchived);
            Assert.AreEqual(15, jsonContent.Balance);
        }

        [Test]
        public async Task Should_return_statuscode_conflict_if_adding_chemical_with_name_that_already_exists()
        {
            // Arrange
            var model = new ChemicalViewModel
            {
                Name = "Chemical2",
                Code = "3456",
                IsArchived = false,
                Balance = 15
            };

            // Act
            var response = await PostAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Test]
        public async Task Should_return_statuscode_conflict_if_adding_chemical_with_code_that_already_exists()
        {
            // Arrange
            var model = new ChemicalViewModel
            {
                Name = "Chemical3",
                Code = "2345",
                IsArchived = false,
                Balance = 15
            };

            // Act
            var response = await PostAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }
    }
}