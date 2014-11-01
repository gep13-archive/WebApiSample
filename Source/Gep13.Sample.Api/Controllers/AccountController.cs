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
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            userManager = userManager;
            roleManager = roleManager;

            ////TODO: This needs to be moved from here.
            userManager.UserValidator = new UserValidator<IdentityUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        [HttpPost]
        [OverrideAuthorization]
        public async Task<IHttpActionResult> Post(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new IdentityUser();
                    Mapper.Map(viewModel, user);

                    if (!roleManager.RoleExists("Member"))
                    {
                        var roleResult = await roleManager.CreateAsync(new IdentityRole("Member"));
                        if (!roleResult.Succeeded)
                        {
                            foreach (var error in roleResult.Errors)
                            {
                                ModelState.AddModelError(error, error);
                            }

                            return BadRequest(ModelState);
                        }
                    }

                    var identityResult = await userManager.CreateAsync(user, viewModel.Password);
                    if (identityResult.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "Member");
                        return Ok();
                    }

                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(error, error);
                    }

                    return BadRequest(ModelState);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
