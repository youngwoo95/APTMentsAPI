using APTMentsAPI.DBModels;
using APTMentsAPI.DTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;

namespace APTMentsAPI.Repository.TheHamBiz
{
    public interface ITheHamBizRepository
    {
        #region 더함비즈 API결과 저장
        /// <summary>
        /// 입차 등록
        /// </summary>
        /// <param name="HistoryTB"></param>
        /// <returns></returns>
        Task<int> AddInCarAsnyc(IoParkingrow RowsTB);

        /// <summary>
        /// 출차 등록
        /// </summary>
        /// <param name="HistoryTB"></param>
        /// <returns></returns>
        Task<int> AddOutCarAsync(IoParkingrow RowsTB);

        /// <summary>
        /// 순찰 등록
        /// </summary>
        /// <param name="PatrolTB"></param>
        /// <param name="PatrolLogList"></param>
        /// <returns></returns>
        Task<int> AddPatrolAsync(Patrolpadlogtb PatrolTB, List<Patrollogtblist> PatrolLogList);
        #endregion
        #region 입-출차 DB
        /// <summary>
        /// 입-출차 리스트 조회
        /// </summary>
        /// <returns></returns>
        //Task<PageNationDTO<InOutViewListDTO>?> InOutViewListAsync();
        //Task<PageNationDTO<InOutViewListDTO>?> InOutViewListAsync(int pageNumber, int pageSize);
        Task<ResponsePage<PageNationDTO<InOutViewListDTO>>?> InOutViewListAsync(int pageNumber, int pageSize, DateTime? startDate, DateTime? EndDate, string? inStatusTpNm, string? CarNumber, string? Dong, string? Ho, int? ParkingDuration, string? ioTicketTpNm);

        /// <summary>
        /// 시퀀스 상세내역 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        Task<List<IoParkingrow>?> DetailViewListAsync(string ioSeq);

        /// <summary>
        /// 차번 최근 7일 조회
        /// </summary>
        /// <param name="carNum"></param>
        /// <param name="ThisTime"></param>
        /// <returns></returns>
        Task<List<IoParkingrow>?> LastWeeksListAsync(string carNum, DateTime ThisTime);

        /// <summary>
        /// View 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<int> UpdateViewMemoAsync(UpdateMemoDTO dto);

        /// <summary>
        /// Row 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        Task<int> UpdateRowMemoAsync(UpdateMemoDTO dto);

        /// <summary>
        /// IO_SEQ 검색
        /// </summary>
        /// <param name="ioseq"></param>
        /// <returns></returns>
        Task<IoParkingrow?> SelectIOSeqInfoAsync(string ioseq);
        #endregion

        /// <summary>
        /// 순찰 List 조회
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ResponsePage<PageNationDTO<PatrolViewListDTO>>?> PatrolViewListAsync(int pageNumber, int pageSize);
    }
}
