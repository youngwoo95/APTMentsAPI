using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.TheHamBizService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APTMentsAPI.Controllers.TheHanBizAPI
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TheHamBizController : ControllerBase
    {
        private readonly IRequestAPI RequestAPIHelpers;
        private readonly ILoggerService LoggerService;
        private readonly ITheHamBizServices TheHamBizServices;

        public TheHamBizController(ILoggerService _loggerservice,
            IRequestAPI _requestapihelpers,
            ITheHamBizServices _thehambizservices)
        {
            this.RequestAPIHelpers = _requestapihelpers;
            this.LoggerService = _loggerservice;
            this.TheHamBizServices = _thehambizservices;
        }

        /*
         출차는 무조건 한번을 보내고,
            입차는 IS_WAIT에 따라 API전송 횟수가 틀려집니다.
            정기차량(입주민) 0으로 전송
            방문차량, 블랙리스트 1로 전송후 입차시 2로 전송
            예약차량 현장옵션에 따라 틀립니다.
            - 예약차량인데 방문증을 뽑고 들어가는 현장: 1로 전송 후 2번 전송
            - 예약차량 자동입차 현장인경우: 0으로 전송
         */

        /// <summary>
        /// 더함비즈 입차 API
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/io/in")]
        public async Task<IActionResult> InCar([FromBody] RequestInTheHamBizDTO? dto)
        {
            try
            {
                
                RequestAPIHelpers.RequestMessage(Request, JsonSerializer.Serialize(dto));

                var model = await TheHamBizServices.AddInCarSerivce(dto).ConfigureAwait(false);
                
                if (model == 1)
                    return Ok(new ResponseDTO() { RES_CD = "1", RES_MSG = "요청이 정상처리되었습니다."});
                else if(model == 0)
                    return Ok(new ResponseDTO() { RES_CD = "0", RES_MSG = "잘못된 요청입니다."});
                else if(model == 2)
                    return Ok(new ResponseDTO() { RES_CD = "2", RES_MSG = "필수값이 누락되었습니다." });
                else
                    return Ok(new ResponseDTO() { RES_CD = "-1", RES_MSG = "서버에서 요청을 처리하지 못하였습니다." });
            }
            catch (Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Ok(new ResponseDTO() { RES_CD = "-1", RES_MSG = "서버에서 요청을 처리하지 못하였습니다." });
            }
        }

        //https://echo.free.beeceptor.com <-- 얘내 진짜 서버
        //http://123.2.156.148:5255/api/TheHamBiz <-- 이 형식으로 해야함
        /// <summary>
        /// 더함비즈 출차 API
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("api/v1/io/out")]
        public async Task<IActionResult> OutCar([FromBody] RequestOutTheHamBizDTO? dto)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request, JsonSerializer.Serialize(dto));

                var model = await TheHamBizServices.AddOutCarService(dto).ConfigureAwait(false);
                if (model == 1)
                    return Ok(new ResponseDTO() { RES_CD = "1", RES_MSG = "요청이 정상처리되었습니다." });
                else if (model == 0)
                    return Ok(new ResponseDTO() { RES_CD = "0", RES_MSG = "잘못된 요청입니다." });
                else if (model == 2)
                    return Ok(new ResponseDTO() { RES_CD = "2", RES_MSG = "필수값이 누락되었습니다." });
                else
                    return Ok(new ResponseDTO() { RES_CD = "-1", RES_MSG = "서버에서 요청을 처리하지 못하였습니다." });
            }
            catch(Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Ok(new ResponseDTO() { RES_CD = "2", RES_MSG = "서버에서 요청을 처리하지 못하였습니다." });
            }
        }

        /// <summary>
        /// 더함비즈 순찰 API
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/patrol")]
        public async Task<IActionResult> PatrolPad([FromBody] RequestPadTheHamBizDTO dto)
        {
            try
            {
                RequestAPIHelpers.RequestMessage(Request, JsonSerializer.Serialize(dto));

                var model = await TheHamBizServices.AddPatrolService(dto).ConfigureAwait(false);
                if (model == 1)
                    return Ok(new ResponseDTO() { RES_CD = "1", RES_MSG = "요청이 정상처리되었습니다." });
                else if (model == 0)
                    return Ok(new ResponseDTO() { RES_CD = "0", RES_MSG = "잘못된 요청입니다." });
                else if (model == 2)
                    return Ok(new ResponseDTO() { RES_CD = "2", RES_MSG = "필수값이 누락되었습니다." });
                else
                    return Ok(new ResponseDTO() { RES_CD = "-1", RES_MSG = "서버에서 요청을 처리하지 못하였습니다." });
            }
            catch(Exception ex)
            {
                LoggerService.FileAPIMessage($"[ERROR]_{ex.ToString()}");
                LoggerService.FileLogMessage(ex.ToString());
                return Ok(new ResponseDTO() { RES_CD = "2", RES_MSG = "서버에서 요청을 처리하지 못하였습니다." });
            }
        }


        /// <summary>
        /// 더함비즈 요구 ResponseDTO
        /// </summary>
        private class ResponseDTO
        {
            /// <summary>
            /// 1. 성공, 2. 필수값 누락
            /// </summary>
            public string? RES_CD { get; set; }

            /// <summary>
            /// 메시지
            /// </summary>
            public string? RES_MSG { get; set; }
        }
    }
}
