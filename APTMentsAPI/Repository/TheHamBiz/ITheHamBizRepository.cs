using APTMentsAPI.DBModels;

namespace APTMentsAPI.Repository.TheHamBiz
{
    public interface ITheHamBizRepository
    {
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

        /// <summary>
        /// IO_SEQ 검색
        /// </summary>
        /// <param name="ioseq"></param>
        /// <returns></returns>
        Task<IoParkingrow?> SelectIOSeqInfoAsync(string ioseq);
    }
}
