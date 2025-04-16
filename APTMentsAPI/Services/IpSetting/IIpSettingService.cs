using APTMentsAPI.DTO.IpDTO;

namespace APTMentsAPI.Services.IpSetting
{
    public interface IIpSettingService
    {
        /// <summary>
        /// IP 설정값 가져오기
        /// </summary>
        /// <returns></returns>
        public Task<ResponseUnit<IpSettingDTO>> GetIpAddress();

        /// <summary>
        /// IP값 저장하기
        /// </summary>
        /// <returns></returns>
        public Task<ResponseUnit<bool>> SetIpAddress(IpSettingDTO dto);
    }
}
