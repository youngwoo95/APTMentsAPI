using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.TheHamBizService;
using Microsoft.AspNetCore.Mvc;

namespace APTMentsAPI.Controllers.TheHanBizAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheHamBizController : ControllerBase
    {
        private ILoggerService LoggerService;
        private ITheHamBizServices TheHamBizServices;

        public TheHamBizController(ILoggerService _loggerservice,
            ITheHamBizServices _thehambizservices)
        {
            this.LoggerService = _loggerservice;
            this.TheHamBizServices = _thehambizservices;
        }

        /// <summary>
        /// 더함비즈 입차 API
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("io/in")]
        public async Task<IActionResult> InCar([FromBody] RequestInTheHamBizDTO dto)
        {
            try
            {
                var model = await TheHamBizServices.AddInCarSerivce(dto).ConfigureAwait(false);
                if (model == 1)
                    return Ok(new ResponseDTO() { RES_CD = "1", RES_MSG = "요청이 정상처리되었습니다."});
                else if(model == 2)
                    return Ok(new ResponseDTO() { RES_CD = "2", RES_MSG = "필수값이 누락되었습니다."});
                else
                    return Ok(new ResponseDTO() { RES_CD = "-1", RES_MSG = "서버에서 요청을 처리하지 못하였습니다." });
            }
            catch (Exception ex)
            {
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
        [Route("io/out")]
        public IActionResult OutCar([FromBody] RequestOutTheHamBizDTO patchDoc)
        {
            try
            {
                
                return Ok(patchDoc);
                //return Ok(new ResponseDTO() { RES_CD = "1", RES_MSG =  });
            }
            catch(Exception ex)
            {
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
        [Route("patrol")]
        public IActionResult PatrolPad([FromBody] RequestPadTheHamBizDTO dto)
        {
            try
            {
                return Ok(dto);
            }
            catch(Exception ex)
            {
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
