namespace Gep13.Sample.Api.IntegrationTests.Chemicals
{
    using System.Net;
    using System.Threading.Tasks;

    using Gep13.Sample.Api.ViewModels;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_hazardinfos : CommonAuthenticatedTestSetup
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
        public async Task Should_return_statuscode_ok_when_updating_hazardinfo_by_chemical_id()
        {
            // Arrange
            this.uri = string.Format("{0}{1}", this.uriBase, "/1/HazardInfo");

            // Get the object that we want to update
            var response = await this.GetAsync();
            var hazardInfo = JsonConvert.DeserializeObject<HazardInfoViewModel>(response.Content.ReadAsStringAsync().Result);
            hazardInfo.Name = "Hazard1";

            // Act
            response = await PutAsync(hazardInfo);
            var jsonContent = JsonConvert.DeserializeObject<HazardInfoViewModel>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Hazard1", jsonContent.Name);
        }

        [Test]
        public async Task Should_return_statuscode_preconditionfailed_when_concurrency_exception_occurs()
        {
            // Arrange
            this.uri = string.Format("{0}{1}", this.uriBase, "/1/HazardInfo");

            var model = new HazardInfoViewModel
            {
                Id = 1,
                Name = "Hazard1",
                RowVersion = "AAAAAAAAAAA="
            };

            // Act
            var response = await PutAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.StatusCode);
        }
    }
}