// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the AccountController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper;

    using Gep13.Sample.Api.ViewModels;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class AccountController : ApiController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

            ////TODO: This needs to be moved from here.
            this.userManager.UserValidator = new UserValidator<IdentityUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        [HttpPost]
        [OverrideAuthorization]
        public async Task<IHttpActionResult> Post(RegisterViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = new IdentityUser();
                    Mapper.Map(viewModel, user);

                    if (!this.roleManager.RoleExists("Member"))
                    {
                        var roleResult = await this.roleManager.CreateAsync(new IdentityRole("Member"));
                        if (!roleResult.Succeeded)
                        {
                            foreach (var error in roleResult.Errors)
                            {
                                this.ModelState.AddModelError(error, error);
                            }

                            return this.BadRequest(this.ModelState);
                        }
                    }

                    var identityResult = await this.userManager.CreateAsync(user, viewModel.Password);
                    if (identityResult.Succeeded)
                    {
                        this.userManager.AddToRole(user.Id, "Member");
                        return this.Ok();
                    }

                    foreach (var error in identityResult.Errors)
                    {
                        this.ModelState.AddModelError(error, error);
                    }

                    return this.BadRequest(this.ModelState);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Exception", ex.Message);
                }
            }

            return this.BadRequest(this.ModelState);
        }
    }
}
