// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationOAuthProvider.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ApplicationOAuthProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;
        private readonly Func<UserManager<IdentityUser>> userManagerFactory;

        public ApplicationOAuthProvider(string publicClientId, Func<UserManager<IdentityUser>> userManagerFactory)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            if (userManagerFactory == null)
            {
                throw new ArgumentNullException("userManagerFactory");
            }

            publicClientId = publicClientId;
            userManagerFactory = userManagerFactory;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var userManager = userManagerFactory())
            {
                var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var authIdentity = await userManager.CreateIdentityAsync(user, context.Options.AuthenticationType);
                var cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
                var properties = CreateProperties(user.UserName, user.Roles.First().RoleId);
                var ticket = new AuthenticationTicket(authIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (context.ClientId != publicClientId)
            {
                return Task.FromResult<object>(null);
            }

            var expectedRootUri = new Uri(context.Request.Uri, new Uri("/"));

            if (expectedRootUri.AbsoluteUri == context.RedirectUri)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        private static AuthenticationProperties CreateProperties(string userName, string role)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
                                                   {
                                                       { "userName", userName }, 
                                                       { "role", role }
                                                   };
            return new AuthenticationProperties(data);
        }
    }
}