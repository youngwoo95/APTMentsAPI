using APTMentsAPI.DTO.APTDTO;

namespace APTMentsAPI.Services.Names
{
    public interface IAptNameService
    {
        /// <summary>
        /// 아파트 명칭 등록 & 변경
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<ResponseUnit<bool>> SetAptName(APTNameDTO dto);

        /// <summary>
        /// 아파트 명칭 가져오기
        /// </summary>
        /// <returns></returns>
        public Task<ResponseUnit<APTNameDTO>> GetAptName();
    }
}
