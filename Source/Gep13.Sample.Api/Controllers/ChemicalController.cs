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
    using System;
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
            var chemicalViewModels = Mapper.Map<IEnumerable<ChemicalDto>, IEnumerable<ChemicalViewModel>>(chemicalService.GetChemicals());
            return Ok(chemicalViewModels);
        }

        public IHttpActionResult Get(int id)
        {
            var chemicalViewModel = Mapper.Map<ChemicalDto, ChemicalViewModel>(chemicalService.GetChemicalById(id));
            return Ok(chemicalViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(ChemicalViewModel chemicalViewModel) 
        {
            if (chemicalViewModel == null)
            {
                throw new ArgumentNullException("chemicalViewModel");
            }

            var item = chemicalService.AddChemical(chemicalViewModel.Name, chemicalViewModel.Code, chemicalViewModel.Balance);

            if (item == null) 
            {
                return StatusCode(HttpStatusCode.Conflict);
            }

            return Created(Url.Link("DefaultApi", new { controller = "Chemical", id = item.Id }), item);
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(ChemicalViewModel chemicalViewModel)
        {
            if (chemicalService.UpdateChemical(Mapper.Map<ChemicalViewModel, ChemicalDto>(chemicalViewModel))) 
            {
                return Ok(chemicalViewModel);
            }

            return StatusCode(HttpStatusCode.Conflict);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Archive(int id)
        {
            if (chemicalService.ArchiveChemical(id))
            {
                return Ok();
            }

            return StatusCode(HttpStatusCode.Conflict);
        }
    }
}