using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.TheHamBizService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace APTMentsAPI.Controllers.TheHamBizAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrolController : ControllerBase
    {
        private readonly IRequestAPI RequestAPIHelpers;
        private readonly ILoggerService LoggerService;
        private readonly ITheHamBizServices TheHamBizServices;

        public PatrolController(ILoggerService _loggerservice,
            IRequestAPI _requestapihelpers,
            ITheHamBizServices _thehambizservices)
        {
            this.RequestAPIHelpers = _requestapihelpers;
            this.LoggerService = _loggerservice;
            this.TheHamBizServices = _thehambizservices;
        }

        [HttpGet]
        [Route("v1/ViewList")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(ResponsePage<PatrolViewListDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PatrolListResponseExample))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "순찰 전체 리스트 조회 (조건추가)",
        Description = "필요한 조건을 넣어서 조회시 - 조건별 동작"
        )]
        public async Task<IActionResult> PatrolViewList([FromQuery][Required] int pageNumber, [FromQuery][Required] int pageSize, DateTime? startDate, DateTime? endDate, string? patrolNm,string? carNumber)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.PatrolViewListService(pageNumber, pageSize, startDate, endDate, patrolNm, carNumber);
                if (model is null)
                    return BadRequest();

                if (model.code == 200)
                    return Ok(model);
                else if (model.code == 400)
                    return BadRequest();
                else
                    return Problem(statusCode: 500);
            }
            catch(Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem(statusCode: 500);
            }
        }

    }
}