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

        private readonly IHubContext<BroadcastHub> HubContext;

        public InOutController(ILoggerService _loggerservice, 
            IRequestAPI _requestapihelpers,
            ITheHamBizServices _thehambizservices,
            IHubContext<BroadcastHub> _hubcontext)
        {
            this.RequestAPIHelpers = _requestapihelpers;
            this.LoggerService = _loggerservice;
            this.TheHamBizServices = _thehambizservices;

            this.HubContext = _hubcontext;
        }

        [HttpGet]
        [Route("v1/test")]
        public async Task<IActionResult> Temp()
        {
            await HubContext.Clients.Group("RoomGroup1").SendAsync("ServerSend", "소켓안의 내용","보내는사람이름").ConfigureAwait(false);
            return Ok();
        }

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
        [SwaggerResponse(200, "성공", typeof(ResponsePage<PageNationDTO<InOutViewListDTO>>))]
        [SwaggerResponseExample(200, typeof(ViewListResponseExample))]
        public async Task<IActionResult> ViewList([FromQuery][Required] int pageNumber, [FromQuery][Required] int pageSize, [FromQuery]DateTime? startDate, [FromQuery]DateTime? endDate, [FromQuery]string? inStatusTpNm, [FromQuery]string? carNumber, [FromQuery]string? dong, [FromQuery]string? ho, [FromQuery]int? parkingDuration, [FromQuery]string? ioTicketTpNm)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.InOutViewListService(pageNumber,pageSize, startDate, endDate, inStatusTpNm, carNumber, dong, ho, parkingDuration, ioTicketTpNm);
                if (model is null)
                    return BadRequest();

                if (model.code == 200)
                    return Ok(model);
                else if(model.code == 400)
                    return BadRequest();
                else
                    return Problem("서버에서 처리할 수 없는 요청입니다.", statusCode: 500);
            }
            catch(Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem("서버에서 처리할 수 없는 요청입니다.", statusCode: 500);
            }
        }

        /// <summary>
        /// 해당 Seq의 상세정보 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/DetailView")]
        [SwaggerResponse(200, "성공", typeof(ResponseUnit<List<DetailViewDTO>>))]
        [SwaggerResponseExample(200, typeof(DetailViewResponseExample))]
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
                return Problem("서버에서 처리할 수 없는 요청 입니다.", statusCode: 500);
            }
        }

        /// <summary>
        /// 차번으로 List 최근 7일 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/ViewLastWeeks")]
        [SwaggerResponse(200, "성공", typeof(ResponseUnit<List<LastWeeksDTO>>))]
        [SwaggerResponseExample(200, typeof(LastViewListResponseExample))]
        public async Task<IActionResult> ViewLastWeeks([FromQuery][Required]string carNumber)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.LastWeeksService(carNumber);
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
                return Problem("서버에서 처리할 수 없는 요청입니다.", statusCode: 500);
            }
        }
        
        [HttpPost]
        [Route("v1/UpdateViewMemo")]
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
                    return Problem("서버에서 처리할 수 없는 요청입니다.", statusCode: 500);
            }
            catch(Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem("서버에서 처리할 수 없는 요청입니다.", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("v1/UpdateRowMemo")]
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
                    return Problem("서버에서 처리할 수 없는 요청입니다.", statusCode: 500);
            }
            catch(Exception ex) 
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem("서버에서 처리할 수 없는 요청입니다.", statusCode: 500);
            }
        }

    }
}
