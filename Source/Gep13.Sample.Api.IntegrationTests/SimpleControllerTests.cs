// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleControllerTests.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the SimpleControllerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.IntegrationTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    using Microsoft.Owin.Testing;

    using NUnit.Framework;

    [TestFixture]
    class SimpleControllerTests
    {
        private TestServer testServer;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            this.testServer = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            this.testServer.Dispose();
        }

        [Test]
        public void WebApiGetAllTest()
        {
            var response = this.testServer.HttpClient.GetAsync("/api/simple").Result;
            var result = response.Content.ReadAsAsync<IEnumerable<string>>().Result;

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("hello", result.First());
            Assert.AreEqual("world", result.Last());
        }

        [Test]
        public void WebApiGetTest()
        {
            var response = this.testServer.HttpClient.GetAsync("/api/simple/1").Result;
            var result = response.Content.ReadAsAsync<string>().Result;

            Assert.AreEqual("hello world", result);
        }
    }
}