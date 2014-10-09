// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Gep13.Sample.Api.Startup))]

namespace Gep13.Sample.Api
{
    /// <summary>
    /// Startup Class used to initiate the startup of the Web API
    /// </summary>
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}