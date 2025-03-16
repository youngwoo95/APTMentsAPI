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
        //public Task<ResponseUnit<PageNationDTO<InOutViewListDTO>?>> InOutViewListService(int pageNumber, int pageSize);
        public Task<ResponseUnit<PageNationDTO<InOutViewListDTO>?>> InOutViewListService(int pageNumber, int PageSize, DateTime? StartDate, DateTime? EndDate, string? CarNumber, string? Dong, string? Ho, int? PackingDuration);

        /// <summary>
        /// 시퀀스 상세내역 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        public Task<ResponseList<DetailViewDTO>?> DetailViewService(string ioSeq);

        /// <summary>
        /// 해당 차량 최근 7일치 조회
        /// </summary>
        /// <param name="carNum"></param>
        /// <returns></returns>
        public Task<ResponseList<LastWeeksDTO>?> LastWeeksService(string carNum);

    }
}
