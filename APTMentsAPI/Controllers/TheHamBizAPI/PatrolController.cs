using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.TheHamBizService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> PatrolViewList([FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await TheHamBizServices.PatrolViewListService(pageNumber, pageSize);
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