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

    using Gep13.Sample.Api.ViewModels;
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
            var viewModel = Mapper.Map<ChemicalDTO, ChemicalViewModel>(this.chemicalService.GetChemicalById(id));
            return this.Ok(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(ChemicalViewModel chemical) 
        {
            var item = this.chemicalService.AddChemical(chemical.Name, chemical.Balance);

            if (item == null) 
            {
                return this.StatusCode(HttpStatusCode.Conflict);
            }

            return this.Created(this.Url.Link("DefaultApi", new { controller = "Chemical", id = item.Id }), item);
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(Service.ChemicalDTO chemicalDto)
        {
            if (this.chemicalService.UpdateChemical(chemicalDto)) 
            {
                return this.Ok(chemicalDto);
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