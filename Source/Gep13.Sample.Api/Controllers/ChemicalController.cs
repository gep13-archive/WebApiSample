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
            var chemicalViewModels = Mapper.Map<IEnumerable<ChemicalDTO>, IEnumerable<ChemicalViewModel>>(this.chemicalService.GetChemicals());
            return this.Ok(chemicalViewModels);
        }

        public IHttpActionResult Get(int id)
        {
            var chemicalViewModel = Mapper.Map<ChemicalDTO, ChemicalViewModel>(this.chemicalService.GetChemicalById(id));
            return this.Ok(chemicalViewModel);
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
            if (this.chemicalService.ArchiveChemical(id))
            {
                return this.Ok();
            }

            return this.StatusCode(HttpStatusCode.Conflict);
        }
    }
}