using APTMentsAPI.DTO;
using APTMentsAPI.DTO.ViewsDTO;
using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.TheHamBizService;
using APTMentsAPI.SignalRHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace APTMentsAPI.Controllers.TheHamBizAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class InOutController : ControllerBase
    {
        private readonly IRequestAPI RequestAPIHelpers;
        private readonly ILoggerService LoggerService;
        private readonly ITheHamBizServices TheHamBizServices;

        //private readonly IHubContext<BroadcastHub> HubContext;

        public InOutController(ILoggerService _loggerservice, 
            IRequestAPI _requestapihelpers,
            ITheHamBizServices _thehambizservices
            //,IHubContext<BroadcastHub> _hubcontext
            )
        {
            this.RequestAPIHelpers = _requestapihelpers;
            this.LoggerService = _loggerservice;
            this.TheHamBizServices = _thehambizservices;

            //this.HubContext = _hubcontext;
        }

        // Socket 테스트
        /*
        [HttpGet]
        [Route("v1/test")]
        public async Task<IActionResult> Temp()
        {
            await HubContext.Clients.Group("RoomGroup1").SendAsync("ServerSend", "소켓안의 내용","보내는사람이름").ConfigureAwait(false);
            return Ok();
        }
        */

        /// <summary>
        /// 전체 ViewList 조회
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="CarNumber"></param>
        /// <param name="Dong"></param>
        /// <param name="Ho"></param>
        /// <param name="PackingDuration"></param>
        /// <returns></returns>
      
        [HttpGet]
        [Route("v1/ViewList")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(ResponsePage<InOutViewListDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ViewListResponseExample))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "입출차 전체 리스트 조회 (조건추가)",
        Description = "필요한 조건을 넣어서 조회시 - 조건별 동작"
        )]
        public async Task<IActionResult> ViewList([FromQuery][Required] int pageNumber, [FromQuery][Required] int pageSize, [FromQuery]DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery]string? ioStatusTpNm, [FromQuery]string? carNumber, [FromQuery]string? dong, [FromQuery]string? ho, [FromQuery]int? parkingDuration, [FromQuery]string? ioTicketTpNm)
        {
            try
            {
                //DateTime startDate1 = DateTime.Parse(startDate);
                //DateTime endDate1 = DateTime.Now;

                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.InOutViewListService(pageNumber,pageSize, startDate, endDate, ioStatusTpNm, carNumber, dong, ho, parkingDuration, ioTicketTpNm);
                if (model is null)
                    return BadRequest();

                if (model.code == 200)
                    return Ok(model);
                else if(model.code == 400)
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

        /// <summary>
        /// 입-출차 전체리스트 반환
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/AllList")]
        [Produces("application/json")]
        public async Task<IActionResult> AllList([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? ioStatusTpNm, [FromQuery] string? carNumber, [FromQuery] string? dong, [FromQuery] string? ho, [FromQuery] int? parkingDuration, [FromQuery] string? ioTicketTpNm)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);
                
                var model = await TheHamBizServices.InOutAllListService(startDate, endDate, ioStatusTpNm, carNumber,dong,ho,parkingDuration, ioTicketTpNm);
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

        /// <summary>
        /// 해당 Seq의 상세정보 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/DetailView")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(ResponsePage<DetailViewDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DetailViewResponseExample))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "해당 시퀀스 상세정보 조회",
        Description = "해당 시퀀스의 상세내역 조회"
        )]
        public async Task<IActionResult> DetailView([FromQuery][Required] string ioSeq)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.DetailViewService(ioSeq);
                if (model is null)
                    return BadRequest();

                if (model.code == 200)
                    return Ok(model);
                else
                    return Ok(model);
            }
            catch (Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem(statusCode: 500);
            }
        }

        /// <summary>
        /// 차번으로 List 최근 7일 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/ViewLastWeeks")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(ResponsePage<LastWeeksDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LastViewListResponseExample))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "해당 차량의 지난 7일 상세정보 조회",
        Description = "해당 차량의 지난 7일 상세정보 조회"
        )]
        public async Task<IActionResult> ViewLastWeeks([FromQuery][Required]string carNumber, [FromQuery][Required] DateTime searchDate)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.LastWeeksService(carNumber, searchDate);
                if (model is null)
                    return BadRequest();

                if (model.code == 200)
                    return Ok(model);
                else
                    return Ok(model);
            }
            catch(Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem(statusCode: 500);
            }
        }
        
        [HttpPost]
        [Route("v1/UpdateViewMemo")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "해당 입-출차 한쌍에 대한 메모건 입력",
        Description = "해당 입-출차 한쌍에 대한 메모건 입력"
        )]
        public async Task<IActionResult> UpdateViewMemo([FromBody]UpdateMemoDTO dto)
        {
            try
            {
                if (dto.pId == 0)
                    return BadRequest();

                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.UpdateViewMemoService(dto);
                if (model is null)
                    return BadRequest();

                if (model.code == 200)
                    return Ok(model);
                else if (model.code == 404)
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

        [HttpPost]
        [Route("v1/UpdateRowMemo")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "해당 입차 or 출차 건에 대한 메모 입력",
        Description = "해당 입차 or 출차 건에 대한 메모 입력"
        )]
        public async Task<IActionResult> UpdateRowsMemo([FromBody]UpdateMemoDTO dto)
        {
            try
            {
                if (dto.pId == 0)
                    return BadRequest();

                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.UpdateRowsMemoService(dto);
                if (model is null)
                    return BadRequest();

                if (model.code == 200)
                    return Ok(model);
                else if (model.code == 404)
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
