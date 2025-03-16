using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.TheHamBizService;
using Microsoft.AspNetCore.Mvc;

namespace APTMentsAPI.Controllers.TheHamBizAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class InOutController : ControllerBase
    {
        private ILoggerService LoggerService;
        private ITheHamBizServices TheHamBizServices;

        public InOutController(ILoggerService _loggerservice, 
            ITheHamBizServices _thehambizservices)
        {
            this.LoggerService = _loggerservice;
            this.TheHamBizServices = _thehambizservices;
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
        [Route("ViewList")]
        public async Task<IActionResult> ViewList([FromQuery]int pageNumber, [FromQuery]int pageSize, [FromQuery]DateTime? startDate, [FromQuery]DateTime? endDate, [FromQuery]string? carNumber, [FromQuery]string? dong, [FromQuery]string? ho, [FromQuery]int? packingDuration)
        {
            try
            {
                var apiUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                LoggerService.FileAPIMessage($"[INFO] >> {apiUrl}");

                var model = await TheHamBizServices.InOutViewListService(pageNumber,pageSize, startDate, endDate, carNumber, dong, ho, packingDuration);
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

        /// <summary>
        /// 해당 Seq의 상세정보 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DetailView")]
        public async Task<IActionResult> DetailView([FromQuery] string ioSeq)
        {
            try
            {
                var apiUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                LoggerService.FileAPIMessage($"[INFO] >> {apiUrl}");

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
        [Route("ViewLastWeeks")]
        public async Task<IActionResult> ViewLastWeeks([FromQuery]string carNumber)
        {
            try
            {
                var apiUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                LoggerService.FileAPIMessage($"[INFO] >> {apiUrl}");

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

      

    }
}
