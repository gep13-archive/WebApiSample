namespace Gep13.Sample.Api.IntegrationTests.Chemicals
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Policy;
    using System.Threading.Tasks;

    using Gep13.Sample.Api.ViewModels;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class When_getting_chemicals : CommonAuthenticatedTestSetup
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
        public async Task Should_return_status_code_ok_response()
        {
            this.uri = this.uriBase;
            var response = await this.GetAsync();
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Should_return_json_response()
        {
            this.uri = this.uriBase;
            var response = await this.GetAsync();
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public async Task Should_return_statuscode_ok_and_chemical_when_getting_chemical_by_id()
        {
            this.uri = string.Format("{0}{1}", this.uriBase, "/1");
            var response = await this.GetAsync();
            var jsonContent = JsonConvert.DeserializeObject<ChemicalViewModel>(response.Content.ReadAsStringAsync().Result);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Chemical1", jsonContent.Name);
            Assert.AreEqual(13, jsonContent.Balance);
        }

        [Test]
        public async Task Should_return_statuscode_ok_when_getting_chemicals()
        {
            this.uri = this.uriBase;
            var response = await this.GetAsync();
            var jsonContent = JsonConvert.DeserializeObject<IEnumerable<ChemicalViewModel>>(response.Content.ReadAsStringAsync().Result);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(2, jsonContent.Count());
        }

        [Test]
        public async Task Should_return_statuscode_notfound_and_null_entity_when_id_doesnt_exist()
        {
            this.uri = string.Format("{0}{1}", this.uriBase, "/13");
            var response = await this.GetAsync();
            var jsonContent = JsonConvert.DeserializeObject<ChemicalViewModel>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsNull(jsonContent);
        }
    }
}