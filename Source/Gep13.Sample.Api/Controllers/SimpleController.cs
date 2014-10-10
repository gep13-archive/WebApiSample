// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleController.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the SimpleController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    public class SimpleController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new[] { "hello", "world" };
        }

        public string Get(int id)
        {
            return "hello world";
        }
    }
}