using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;

namespace APTMentsAPI.Services.TheHamBizService
{
    public interface ITheHamBizServices
    {
        /// <summary>
        /// 입차 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<int> AddInCarSerivce(RequestInTheHamBizDTO dto);

        /// <summary>
        /// 출차 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<int> AddOutCarService(RequestOutTheHamBizDTO dto);

    }
}
