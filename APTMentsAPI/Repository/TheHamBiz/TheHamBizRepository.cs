using APTMentsAPI.DBModels;
using APTMentsAPI.Services.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging.Abstractions;

namespace APTMentsAPI.Repository.TheHamBiz
{
    public class TheHamBizRepository : ITheHamBizRepository
    {
        private readonly ILoggerService LoggerService;
        private readonly AptContext Context;

        public TheHamBizRepository(AptContext _context, ILoggerService _loggerservice)
        {
            this.Context = _context;
            this.LoggerService = _loggerservice;
        }

        /// <summary>
        /// IOSEQ 검색
        /// </summary>
        /// <param name="ioseq"></param>
        /// <returns></returns>
        public async Task<IoParkingHistory?> SelectIOSeqInfoAsync(string ioseq)
        {
            try
            {
                var model = await Context.IoParkingHistories.FirstOrDefaultAsync(m => m.IoSeq == ioseq && m.IoGubun == 1);
                if (model is not null)
                    return model;
                else
                    return null;
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 입차 등록
        /// </summary>
        /// <param name="HistoryTB"></param>
        /// <returns></returns>
        public async Task<int> AddInCar(IoParkingHistory HistoryTB)
        {
            try
            {
                using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    int result = 0;

                    // View 먼저 넣고
                    var ViewTB = new IoParkingViewTb
                    {
                        IoSeq = HistoryTB.IoSeq, // 입출차 일련번호
                        InTicketTp = HistoryTB.IoTicketTp, // 입차 차량 구분
                        InTicketTpNm = HistoryTB.IoTicketTpNm, // 입차 차량 구분 명
                        IoStatusTp = HistoryTB.IoStatusTp, // 입출 상태
                        IoStatusTpNm = HistoryTB.IoStatusTpNm, // 입출 상태명
                        CarNum = HistoryTB.CarNum, // 차량번호
                        InDtm = HistoryTB.IoDtm, // 입차시간
                        ParkId = HistoryTB.ParkId, // 주차장ID
                        InGateId = HistoryTB.IoGateId, // 입차 게이트 ID
                        InGateNm = HistoryTB.IoGateNm, // 입차 게이트 명
                        Dong = HistoryTB.Dong, // 동
                        Ho = HistoryTB.Ho, // 호
                        CreateDt = HistoryTB.CreateDt, // 시스템 시간
                        ImagePath1 = HistoryTB.ImgPath, // 입차 사진
                    };


                    // 외래키를 History에 넣어야함.
                    await Context.IoParkingViewTbs.AddAsync(ViewTB).ConfigureAwait(false);
                    result = await Context.SaveChangesAsync();
                    if(result < 1)
                    {
                        await transaction.RollbackAsync();
                        return -1;
                    }

                    HistoryTB.SPid = ViewTB.Pid;
                    await Context.IoParkingHistories.AddAsync(HistoryTB).ConfigureAwait(false);
                    result = await Context.SaveChangesAsync();
                    if(result < 1)
                    {
                        await transaction.RollbackAsync();
                        return -1; // 저장실패
                    }


                    LoggerService.ConsoleLogMessage("저장이 완료되었습니다.");
                    await transaction.CommitAsync();
                    return result;
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        
    }
}
