namespace Gep13.Sample.Api.IntegrationTests.Chemicals
{
    using System.Net;
    using System.Threading.Tasks;

    using NUnit.Framework;

    [TestFixture]
    public class When_archiving_chemicals : CommonAuthenticatedTestSetup
    {
        private string uriBase = "/api/Chemical";
        private string uri = string.Empty;

        protected override string Uri
        {
            get { return uri; }
        }

        [Test]
        public async Task Should_return_statuscode_ok()
        {
            // Arrange
            this.uri = string.Format("{0}{1}", this.uriBase, "/1");

            // Act
            var response = await this.DeleteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Should_return_statuscode_conflict_if_deleting_chemical_that_doesnt_exist()
        {
            // Arrange
            this.uri = string.Format("{0}{1}", this.uriBase, "/3");

            // Act
            var response = await this.DeleteAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }
    }
}