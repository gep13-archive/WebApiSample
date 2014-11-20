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
            var databaseOperation = this.chemicalService.GetChemicals();

            switch (databaseOperation.Status)
            {
                case DatabaseOperationStatus.Success:
                    var chemicalViewModels = Mapper.Map<IEnumerable<ChemicalDto>, IEnumerable<ChemicalViewModel>>(databaseOperation.Result);
                    return Ok(chemicalViewModels);
                default:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        public IHttpActionResult Get(int id)
        {
            var databaseOperation = this.chemicalService.GetChemicalById(id);

            switch (databaseOperation.Status)
            {
                case DatabaseOperationStatus.NotFound:
                    return this.StatusCode(HttpStatusCode.NotFound);
                case DatabaseOperationStatus.Success:
                    var chemicalViewModel = Mapper.Map<ChemicalDto, ChemicalViewModel>(databaseOperation.Result);
                    return this.Ok(chemicalViewModel);
                default:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Chemical/{id}/HazardInfo")]
        public IHttpActionResult GetHazardInfo(int id)
        {
            var databaseOperation = this.chemicalService.GetHazardInfoForChemicalId(id);

            switch (databaseOperation.Status)
            {
                case DatabaseOperationStatus.NotFound:
                    return this.StatusCode(HttpStatusCode.NotFound);
                case DatabaseOperationStatus.Success:
                    var chemicalViewModel = Mapper.Map<HazardInfoDto, HazardInfoViewModel>(databaseOperation.Result);
                    return this.Ok(chemicalViewModel);
                default:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(ChemicalViewModel chemicalViewModel)
        {
            if (chemicalViewModel == null)
            {
                throw new ArgumentNullException("chemicalViewModel");
            }

            var databaseOperation = this.chemicalService.AddChemical(chemicalViewModel.Name, chemicalViewModel.Code, chemicalViewModel.Balance);

            switch (databaseOperation.Status)
            {
                case DatabaseOperationStatus.Conflict:
                    return this.StatusCode(HttpStatusCode.Conflict);
                case DatabaseOperationStatus.Success:
                    return Created(Url.Link("DefaultApi", new { controller = "Chemical", id = databaseOperation.Result.Id }), databaseOperation.Result);
                case DatabaseOperationStatus.Exception:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
                default:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(ChemicalViewModel chemicalViewModel)
        {
            var databaseOperation = this.chemicalService.UpdateChemical(Mapper.Map<ChemicalViewModel, ChemicalDto>(chemicalViewModel));
            
            switch (databaseOperation.Status)
            {
                case DatabaseOperationStatus.Conflict:
                    return this.StatusCode(HttpStatusCode.Conflict);
                case DatabaseOperationStatus.Success:
                    return this.Ok(databaseOperation.Result);
                case DatabaseOperationStatus.ConcurrencyProblem:
                    return this.StatusCode(HttpStatusCode.PreconditionFailed);
                default:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Chemical/{id}/HazardInfo")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(int id, HazardInfoViewModel hazardInfoViewModel)
        {
            var databaseOperation = this.chemicalService.UpdateHazardInfo(id, Mapper.Map<HazardInfoViewModel, HazardInfoDto>(hazardInfoViewModel));

            switch (databaseOperation.Status)
            {
                case DatabaseOperationStatus.Conflict:
                    return this.StatusCode(HttpStatusCode.Conflict);
                case DatabaseOperationStatus.Success:
                    return this.Ok(databaseOperation.Result);
                case DatabaseOperationStatus.ConcurrencyProblem:
                    return this.StatusCode(HttpStatusCode.PreconditionFailed);
                default:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Archive(int id)
        {
            var databaseOperation = this.chemicalService.ArchiveChemical(id);

            switch (databaseOperation)
            {
                case DatabaseOperationStatus.Success:
                    return this.Ok();
                case DatabaseOperationStatus.NotFound:
                    return this.StatusCode(HttpStatusCode.NotFound);
                default:
                    return this.StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}