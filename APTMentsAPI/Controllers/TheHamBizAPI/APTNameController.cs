using APTMentsAPI.DTO.APTDTO;
using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.Names;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APTMentsAPI.Controllers.TheHamBizAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class APTNameController : ControllerBase
    {
        private readonly IRequestAPI RequestAPIHelpers;
        private readonly ILoggerService LoggerService;
        private readonly IAptNameService AptNameService;

        public APTNameController(ILoggerService _loggerservice, IRequestAPI _requestapi, IAptNameService _aptnameservice)
        {
            this.RequestAPIHelpers = _requestapi;
            this.LoggerService = _loggerservice;
            this.AptNameService = _aptnameservice;
        }

        // get
        [HttpGet]
        [Produces("application/json")]
        [Route("v1/GetName")]
        public async Task<IActionResult> GetName()
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request);

                var model = await AptNameService.GetAptName();
             
                if (model.code == 204)
                    return NoContent();
                else if(model.code == 200)
                    return Ok(model);
                else
                    return Problem(statusCode: 500);
            }
            catch (Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem(statusCode: 500);
            }
        }

        //set
        [HttpPost]
        [Produces("application/json")]
        [Route("v1/SetName")]
        public async Task<IActionResult> SetName([FromBody]APTNameDTO dto)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request, JsonSerializer.Serialize(dto));

                var model = await AptNameService.SetAptName(dto);

                if (model.code == 204)
                    return NoContent();
                else if (model.code == 200)
                    return Ok(model);
                return Problem(statusCode: 500);

            }
            catch (Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Problem(statusCode: 500);
            }
        }

    }
}
