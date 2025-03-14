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

        [HttpGet]
        [Route("ViewList")]
        public async Task<IActionResult> ViewList([FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
            try
            {
                var apiUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                LoggerService.FileAPIMessage($"[INFO] >> {apiUrl}");

                var model = await TheHamBizServices.InOutViewListService(pageNumber,pageSize);
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

        // 차번으로 List 최근 7일 or 최근 몇건 검색 또는 [해당 Seq로 상세정보 조회- 이거였던것 같음.]


    }
}
