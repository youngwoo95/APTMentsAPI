using APTMentsAPI.DTO.IpDTO;
using APTMentsAPI.Repository.TheHamBiz;
using APTMentsAPI.Services.Logger;

namespace APTMentsAPI.Services.IpSetting
{
    public class IpSettingService : IIpSettingService
    {
        private readonly ILoggerService LoggerService;
        private readonly ITheHamBizRepository TheHamBizRepository;

        public IpSettingService(ILoggerService _loggerservice,
            ITheHamBizRepository _thehambizrepository)
        {
            this.LoggerService = _loggerservice;
            this.TheHamBizRepository = _thehambizrepository;
        }

        /// <summary>
        /// IP 설정값 가져오기
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseUnit<IpSettingDTO>> GetIpAddress()
        {
            try
            {
                var model = await TheHamBizRepository.GetIpAddressInfoAsync();
                if (model is null)
                    return new ResponseUnit<IpSettingDTO>() { data = null, code = 204 };
                else
                {
                    return new ResponseUnit<IpSettingDTO>()
                    {
                        data = new IpSettingDTO
                        {
                            IpAddress = model.IpAddress
                        },
                        code = 200
                    };
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponseUnit<IpSettingDTO>() { data = null, code = 500 };
            }
        }

        /// <summary>
        /// IP 설정값 저장하기
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseUnit<bool>> SetIpAddress(IpSettingDTO dto)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(dto.IpAddress))
                    return new ResponseUnit<bool>() { data = false, code = 204 };

                var model = await TheHamBizRepository.SetIpAddressInfoAsync(dto.IpAddress);
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
