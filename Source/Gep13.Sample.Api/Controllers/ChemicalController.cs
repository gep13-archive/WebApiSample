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
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using AutoMapper;

    using Gep13.Sample.Api.ViewModels;
    using Gep13.Sample.Model;
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
            var chemicalViewModels = new List<ChemicalViewModel>();
            Mapper.Map(chemicals, chemicalViewModels);
            return this.Ok(chemicalViewModels);
        }

        public IHttpActionResult Get(int id)
        {
            var chemical = this.chemicalService.GetChemicalById(id);
            var viewModel = new ChemicalViewModel();
            Mapper.Map(chemical, viewModel);
            return this.Ok(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(ChemicalViewModel chemicalViewModel)
        {
            var found = this.chemicalService.GetChemicalByName(chemicalViewModel.Name);
            if (found.ToList().Count == 0)
            {
                var entity = new Chemical();
                Mapper.Map(chemicalViewModel, entity);
                try
                {
                    this.chemicalService.AddChemical(entity);
                    Mapper.Map(entity, chemicalViewModel);
                }
                catch
                {
                    return this.StatusCode(HttpStatusCode.Conflict);
                }

                return this.Created(this.Url.Link("DefaultApi", new { controller = "Chemical", id = chemicalViewModel.Id }), chemicalViewModel);
            }

            return this.StatusCode(HttpStatusCode.Conflict);
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(ChemicalViewModel chemicalViewModel)
        {
            var found = this.chemicalService.GetChemicalByName(chemicalViewModel.Name);
            if (found.ToList().Count == 0)
            {
                var chemical = this.chemicalService.GetChemicalById(chemicalViewModel.Id);
                Mapper.Map(chemicalViewModel, chemical);
                this.chemicalService.UpdateChemical(chemical);
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