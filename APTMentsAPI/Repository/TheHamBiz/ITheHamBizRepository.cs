using APTMentsAPI.DBModels;
using APTMentsAPI.DTO;
using APTMentsAPI.DTO.APTDTO;
using APTMentsAPI.DTO.IpDTO;
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
        Task<int> AddPatrolAsync(List<Patrolpadlogtb> PatrolTB);
        #endregion
        #region 입-출차 DB
        /// <summary>
        /// 입-출차 리스트 조회
        /// </summary>
        /// <returns></returns>
        Task<ResponsePage<InOutViewListDTO>?> InOutViewListAsync(int pageNumber, int pageSize, DateTime? startDate, DateTime? EndDate, string? ioStatusTpNm, string? CarNumber, string? Dong, string? Ho, int? ParkingDuration, string? ioTicketTpNm);

        /// <summary>
        /// 입-출차 전체 리스트 조회
        /// </summary>
        /// <returns></returns>
        Task<ResponsePage<InOutViewListDTO>?> InOutAllListAsync(DateTime? StartDate, DateTime? EndDate, string? ioStatusTpNm, string? CarNumber, string? Dong, string? Ho, int? ParkingDuration, string? ioTicketTpNm);

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
        Task<ResponsePage<PatrolViewListDTO>?> PatrolViewListAsync(int pageNumber, int pageSize, DateTime? startDate, DateTime? endDate, string? patrolNm, string? carNumber);

        /// <summary>
        /// 순찰 전체 조회
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="patrolNm"></param>
        /// <param name="carNumber"></param>
        /// <returns></returns>
        Task<ResponsePage<PatrolViewListDTO>?> PatrolAllListAsync(DateTime? startDate, DateTime? endDate, string? patrolNm, string? carNumber);
        

        /// <summary>
        /// 아파트명칭 가져오기
        /// </summary>
        /// <returns></returns>
        Task<Apartmentname?> GetAptNameInfoAsync();

        /// <summary>
        /// 아파트 명칭 설정하기
        /// </summary>
        /// <returns></returns>
        Task<int> SetAptNameInfoAsync(string aptName);

        /// <summary>
        /// IP 설정값 가져오기
        /// </summary>
        /// <returns></returns>
        Task<Ipsetting?> GetIpAddressInfoAsync();

        /// <summary>
        /// IP 설정값 저장하기
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        Task<int> SetIpAddressInfoAsync(string ipaddress);
    }
}
