namespace Gep13.Sample.Api.IntegrationTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;

    using Microsoft.Owin.Testing;

    using Newtonsoft.Json.Linq;

    using NUnit.Framework;

    public abstract class CommonAuthenticatedTestSetup : CommonTestSetup
    {
        protected virtual string Username { get { return "gep13@gep13.co.uk"; } }
        protected virtual string Password { get { return "Password01"; } }

        private string token;

        protected override void PostSetup(TestServer server)
        {
            var tokenDetails = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Password)
                };

            var tokenPostData = new FormUrlEncodedContent(tokenDetails);
            var tokenResult = server.HttpClient.PostAsync("/Token", tokenPostData).Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResult.StatusCode);

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            token = (string)body["access_token"];
        }

        protected async Task<TResult> GetAsync<TResult>()
        {
            var response = await GetAsync();
            return await response.Content.ReadAsAsync<TResult>();
        }

        protected override async Task<HttpResponseMessage> GetAsync()
        {
            return await this.TestServer.CreateRequest(Uri)
                .AddHeader("Authorization", "Bearer " + token)
                .GetAsync();
        }

        protected override async Task<HttpResponseMessage> PostAsync<TModel>(TModel model)
        {
            return await this.TestServer.CreateRequest(Uri)
                .AddHeader("Authorization", "Bearer " + token)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .PostAsync();
        }

        protected override async Task<HttpResponseMessage> PutAsync<TModel>(TModel model)
        {
            return await this.TestServer.CreateRequest(Uri)
                .AddHeader("Authorization", "Bearer " + token)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .And(request => request.Method = HttpMethod.Put)
                .SendAsync("PUT");
        }

        protected override async Task<HttpResponseMessage> DeleteAsync()
        {
            return await this.TestServer.CreateRequest(Uri)
                .AddHeader("Authorization", "Bearer " + token)
                .And(request => request.Method = HttpMethod.Delete)
                .SendAsync("DELETE");
        }
    }
}