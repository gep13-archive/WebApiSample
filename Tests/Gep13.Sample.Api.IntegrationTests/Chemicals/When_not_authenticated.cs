namespace Gep13.Sample.Api.IntegrationTests.Chemicals
{
    using System.Net;
    using System.Threading.Tasks;

    using Gep13.Sample.Api.ViewModels;

    using NUnit.Framework;

    [TestFixture]
    public class When_not_authenticated : CommonTestSetup
    {
        private string uriBase = "/api/Chemical";
        private string uri = string.Empty;

        protected override string Uri
        {
            get { return uri; }
        }

        [Test]
        public async Task Insert_should_return_not_authorized()
        {
            // Arrange
            var model = new ChemicalViewModel
            {
                Id = 3,
                Name = "Chemical3",
                IsArchived = false,
                Balance = 15
            };

            this.uri = this.uriBase;

            // Act
            var response = await PostAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Update_should_return_not_authorized()
        {
            // Arrange
            var model = new ChemicalViewModel
            {
                Id = 1,
                Name = "Chemical1",
                IsArchived = false,
                Balance = 14
            };

            this.uri = this.uriBase;

            // Act
            var response = await PutAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Delete_should_return_not_authorized()
        {
            // Arrange
            this.uri = string.Format("{0}{1}", this.uriBase, "/1");

            // Act
            var response = await DeleteAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}