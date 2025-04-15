using APTMentsAPI.DTO.IpDTO;
using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.IpSetting;
using APTMentsAPI.Services.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APTMentsAPI.Controllers.TheHamBizAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpSettingController : ControllerBase
    {
        private readonly IRequestAPI RequestAPIHelpers;
        private readonly ILoggerService LoggerService;
        private readonly IIpSettingService IpSettingService;

        public IpSettingController(ILoggerService _loggerservice, IRequestAPI _requestapi, IIpSettingService _ipsettingservice)
        {
            this.RequestAPIHelpers = _requestapi;
            this.LoggerService = _loggerservice;
            this.IpSettingService = _ipsettingservice;
        }

        // get
        [HttpGet]
        [Produces("application/json")]
        [Route("v1/GetIpAddress")]
        public async Task<IActionResult> GetIpAddress()
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await IpSettingService.GetIpAddress();

                if (model.code == 204)
                    return NoContent();
                else if (model.code == 200)
                    return Ok(model);
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

        // set
        [HttpPost]
        [Produces("application/json")]
        [Route("v1/setIpAddress")]
        public async Task<IActionResult> SetIpAddress([FromBody]IpSettingDTO dto)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request, JsonSerializer.Serialize(dto));

                var model = await IpSettingService.SetIpAddress(dto);

                if (model.code == 204)
                    return NoContent();
                else if (model.code == 200)
                    return Ok(model);
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
