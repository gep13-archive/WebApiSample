// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalController.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using AutoMapper;
    using Gep13.Sample.Service;

    public class ChemicalController : ApiController
    {
        private readonly IChemicalService chemicalService;

        public ChemicalController(IChemicalService chemicalService)
        {
            this.chemicalService = chemicalService;
        }

        public IHttpActionResult Get()
        {
            var chemicals = this.chemicalService.GetChemicals();
            var chemicalViewModels = new List<ViewModels.ChemicalViewModel>();
            Mapper.Map(chemicals, chemicalViewModels);
            return this.Ok(chemicalViewModels);
        }

        public IHttpActionResult Get(int id)
        {
            return this.Ok(this.chemicalService.GetChemicalById(id));
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(Service.ChemicalViewModel chemicalViewModel) {

            var item = chemicalService.AddChemical(chemicalViewModel);

            if (item == null) {
                return this.StatusCode(HttpStatusCode.Conflict);
            }

            return this.Created(this.Url.Link("DefaultApi", new { controller = "Chemical", id = chemicalViewModel.Id }), chemicalViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(Service.ChemicalViewModel chemicalViewModel)
        {

            if (chemicalService.UpdateChemical(chemicalViewModel)) {
                return this.Ok(chemicalViewModel);
            }

            return this.StatusCode(HttpStatusCode.Conflict);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Archive(int id)
        {
            var chemical = this.chemicalService.GetChemicalById(id);
            chemical.IsArchived = true;
            this.chemicalService.UpdateChemical(chemical);
            return this.Ok();
        }
    }
}