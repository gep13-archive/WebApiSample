namespace Gep13.Sample.Api.IntegrationTests
{
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;

    using Gep13.Sample.Api.Mappers;
    using Gep13.Sample.Common;

    using Microsoft.Owin.Testing;

    using NUnit.Framework;

    public abstract class CommonTestSetup
    {
        private const string ConnectionName = "Gep13";

        private readonly ScriptRunner scriptRunner;

        protected TestServer TestServer;

        protected CommonTestSetup()
        {
            scriptRunner = ScriptRunner.Get(ConnectionName);
        }

        [TestFixtureSetUp]
        public void InitializeTests()
        {
            AutoMapperConfiguration.Configure();
            this.TestServer = TestServer.Create<Startup>();

            scriptRunner.DropDb("gep13_IntegrationTests");
            scriptRunner.CreateDb("gep13_IntegrationTests");

            scriptRunner.Run(@"..\..\Database Scripts\Script0001 - Create Identity Tables.sql");
            scriptRunner.Run(@"..\..\Database Scripts\Script0002 - Create Chemical Table.sql");
            scriptRunner.Run(@"..\..\Database Scripts\Script0003 - Create HazardInfos Table.sql");
            scriptRunner.Run(@"..\..\Database Scripts\Script0501 - Create Sample Users.sql");
            scriptRunner.Run(@"..\..\Database Scripts\Script0502 - Create Sample Chemical Data.sql");
            scriptRunner.Run(@"..\..\Database Scripts\Script0503 - Create Sample HazardInfo Data.sql");

            PostSetup(this.TestServer);
        }

        [TestFixtureTearDown]
        public void FinalizeTests()
        {
            this.TestServer.Dispose();
        }

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        protected abstract string Uri { get; }

        protected virtual void PostSetup(TestServer server)
        {
        }

        protected virtual async Task<HttpResponseMessage> GetAsync()
        {
            return await this.TestServer.CreateRequest(Uri).GetAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostAsync<TModel>(TModel model)
        {
            return await this.TestServer.CreateRequest(Uri)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .PostAsync();
        }

        protected virtual async Task<HttpResponseMessage> PutAsync<TModel>(TModel model)
        {
            return await this.TestServer.CreateRequest(Uri)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .SendAsync("PUT");
        }

        protected virtual async Task<HttpResponseMessage> DeleteAsync()
        {
            return await this.TestServer.CreateRequest(Uri)
                .And(request => request.Method = HttpMethod.Delete)
                .SendAsync("DELETE");
        }
    }
}