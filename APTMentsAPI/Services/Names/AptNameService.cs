using APTMentsAPI.DTO.APTDTO;
using APTMentsAPI.Repository.TheHamBiz;
using APTMentsAPI.Services.Logger;

namespace APTMentsAPI.Services.Names
{
    public class AptNameService : IAptNameService
    {
        private readonly ILoggerService LoggerService;
        private readonly ITheHamBizRepository TheHamBizRepository;

        public AptNameService(ILoggerService _loggerservice, 
            ITheHamBizRepository _thehambizrepository)
        {
            this.LoggerService = _loggerservice;
            this.TheHamBizRepository = _thehambizrepository;
        }

        /// <summary>
        /// 아파트 명칭 가져오기
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseUnit<APTNameDTO>> GetAptName()
        {
            try
            {
                var model = await TheHamBizRepository.GetAptNameInfoAsync();
                if (model is null)
                    return new ResponseUnit<APTNameDTO>() { data = null, code = 204 };
                else
                {
                    return new ResponseUnit<APTNameDTO>()
                    {
                        data = new APTNameDTO
                        {
                            APTName = model.Aptname
                        },
                        code = 200
                    };
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponseUnit<APTNameDTO>() { data = null, code = 500 };
            }
        }

        /// <summary>
        /// 아파트 명칭 설정하기
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseUnit<bool>> SetAptName(APTNameDTO dto)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(dto.APTName))
                    return new ResponseUnit<bool>() { data = false, code = 204 };

                var model = await TheHamBizRepository.SetAptNameInfoAsync(dto.APTName);
                if (model == -1)
                    return new ResponseUnit<bool>() { data = false, code = 500 };
                else
                    return new ResponseUnit<bool>() { data = true, code = 200 };
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponseUnit<bool>() { data = false, code = 500 };
            }
        }
    }
}
