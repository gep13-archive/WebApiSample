// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalControllerIntegrationTests.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalControllerIntegrationTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.IntegrationTests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;

    using AutoMapper;

    using Microsoft.Owin.Testing;

    using NUnit.Framework;

    public class When_getting_chemicals
    {
        private static Assembly[] assemblies = { Assembly.Load("Gep13.Sample.Api"), Assembly.Load("Gep13.Sample.Service") };
        private TestServer testServer;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            SetupAutoMapper();
            this.testServer = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            this.testServer.Dispose();
        }

        [Test]
        public void Should_get_statuscodeok_response()
        {
            var response = this.testServer.HttpClient.GetAsync("/api/Chemical").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Should_get_json_response()
        {
            var response = this.testServer.HttpClient.GetAsync("/api/Chemical").Result;
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }
        private static void SetupAutoMapper()
        {
            Mapper.Reset();

            Mapper.Initialize(cfg =>
            {
                foreach (var assembly in assemblies)
                {
                    var profiles = assembly.GetTypes()
                        .Where(t => (t.IsSubclassOf(typeof(Profile)) && t.GetConstructor(Type.EmptyTypes) != null))
                        .Select(p => (Profile)Activator.CreateInstance(p));

                    foreach (var item in profiles)
                    {
                        cfg.AddProfile(item);
                    }
                }

                cfg.AllowNullDestinationValues = true;
            });
        }
    }
}