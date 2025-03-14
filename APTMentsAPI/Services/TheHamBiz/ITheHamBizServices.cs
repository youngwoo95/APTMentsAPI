using APTMentsAPI.DTO;
using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;

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

        /// <summary>
        /// 순찰 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<int> AddPatrolService(RequestPadTheHamBizDTO dto);

        /// <summary>
        /// 입-출차 리스트
        /// </summary>
        /// <returns></returns>
        public Task<ResponseUnit<PageNationDTO<InOutViewListDTO>?>> InOutViewListService(int pageNumber, int pageSize);




    }
}
