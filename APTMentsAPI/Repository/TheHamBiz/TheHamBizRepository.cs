using APTMentsAPI.DBModels;
using APTMentsAPI.Services.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
        public async Task<IoParkingrow?> SelectIOSeqInfoAsync(string ioseq)
        {
            try
            {
                var model = await Context.IoParkingrows.FirstOrDefaultAsync(m => m.IoSeq == ioseq && m.IoGubun == 1);
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
        public async Task<int> AddInCarAsnyc(IoParkingrow RowsTB)
        {
            try
            {
                using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    int result = 0;

                    await Context.IoParkingrows.AddAsync(RowsTB).ConfigureAwait(false);
                    result = await Context.SaveChangesAsync().ConfigureAwait(false);
                    if(result < 1)
                    {
                        await transaction.RollbackAsync().ConfigureAwait(false);
                        return -1;
                    }

                    var ViewTableCheck = await Context.IoParkingviewtbs.FirstOrDefaultAsync(m => m.IoSeq == RowsTB.IoSeq);
                    if(ViewTableCheck is null) // View 테이블엔 없다.
                    {
                        // RowsTB 의 isWait 가 == 1이다 --> VIewTB 안넣는다.
                        // 1이 아니다 넣는다.
                        if(RowsTB.IsWait != "1")
                        {
                            var ViewTB = new IoParkingviewtb
                            {
                                IoSeq = RowsTB.IoSeq, // 시퀀스 번호
                                InPid = RowsTB.Pid, // 최종 입차 INDEX
                                InDtm = RowsTB.IoDtm, // 입차 시간
                                CarNum = RowsTB.CarNum, // 차량 번호
                                Dong = RowsTB.Dong, // 동
                                Ho = RowsTB.Ho // 호
                            };

                            await Context.IoParkingviewtbs.AddAsync(ViewTB).ConfigureAwait(false);
                            result = await Context.SaveChangesAsync().ConfigureAwait(false);

                            if(result < 1)
                            {
                                await transaction.RollbackAsync().ConfigureAwait(false);
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        // 이미 있는데 RowsTB가 1이 아니다
                        if(RowsTB.IsWait != "1")
                        {
                            // 그것이 0 or 2 
                            // update - ViewTB의 입차 Index를 바꿔줘야한다.
                            ViewTableCheck.InPid = RowsTB.Pid; // 최종 입차 INDEX
                            ViewTableCheck.InDtm = RowsTB.IoDtm; // 최종 입차 시간
                            ViewTableCheck.CarNum = RowsTB.CarNum; // 차량 번호
                            ViewTableCheck.Dong = RowsTB.Dong; // 동
                            ViewTableCheck.Ho = RowsTB.Ho;

                            Context.IoParkingviewtbs.Update(ViewTableCheck);
                            result = await Context.SaveChangesAsync().ConfigureAwait(false);

                            if(result < 1)
                            {
                                await transaction.RollbackAsync().ConfigureAwait(false);
                                return -1;
                            }
                        }
                    }
                    await transaction.CommitAsync().ConfigureAwait(false);
                    return 1;
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 출차 등록
        /// </summary>
        /// <param name="HistoryTB"></param>
        /// <returns></returns>
        public async Task<int> AddOutCarAsync(IoParkingrow RowsTB) 
        {
            try
            {
                using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    int result = 0;


                    await Context.IoParkingrows.AddAsync(RowsTB).ConfigureAwait(false);
                    result = await Context.SaveChangesAsync().ConfigureAwait(false);

                    if(result < 1)
                    {
                        await transaction.RollbackAsync().ConfigureAwait(false);
                        return -1;
                    }

                    var ViewTableCheck = await Context.IoParkingviewtbs.FirstOrDefaultAsync(m => m.IoSeq == RowsTB.IoSeq);
                    
                    // 입차 기록이 없음 - 대게 프로그램 처음 실행시켰을때임.
                    if (ViewTableCheck is null) 
                    {
                        var ViewTB = new IoParkingviewtb
                        {
                            IoSeq = RowsTB.IoSeq, // 시퀀스 번호
                            OutPid = RowsTB.Pid, // 최종 출차 INDEX
                            OutDtm = RowsTB.IoDtm, // 출차 시간
                            CarNum = RowsTB.CarNum, // 차량 번호
                            Dong = RowsTB.Dong, // 동
                            Ho = RowsTB.Ho,
                        };

                        await Context.IoParkingviewtbs.AddAsync(ViewTB).ConfigureAwait(false);
                        result = await Context.SaveChangesAsync().ConfigureAwait(false);

                        if(result < 1)
                        {
                            await transaction.RollbackAsync().ConfigureAwait(false);
                            return -1;
                        }
                    }
                    else
                    {
                        // 내용이 있음 --> 업데이트
                        ViewTableCheck.OutPid = RowsTB.Pid; // 최종 출차 INDEX
                        ViewTableCheck.OutDtm = RowsTB.IoDtm;// 최종 출차 시간
                        ViewTableCheck.CarNum = RowsTB.CarNum; // 차량 번호
                        ViewTableCheck.Dong = RowsTB.Dong; // 동
                        ViewTableCheck.Ho = RowsTB.Ho; // 호

                        Context.IoParkingviewtbs.Update(ViewTableCheck);
                        result = await Context.SaveChangesAsync().ConfigureAwait(false);

                        if(result < 1)
                        {
                            await transaction.RollbackAsync().ConfigureAwait(false);
                            return -1;
                        }
                    }

                    await transaction.CommitAsync().ConfigureAwait(false);
                    return 1;
                }
            }catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 순찰 등록
        /// </summary>
        /// <param name="PatrolTB"></param>
        /// <param name="PatrolLogList"></param>
        /// <returns></returns>
        public async Task<int> AddPatrolAsync(Patrolpadlogtb PatrolTB, List<Patrollogtblist> PatrolLogList)
        {
            try
            {
                using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    int result = 0;

                    await Context.Patrolpadlogtbs.AddAsync(PatrolTB).ConfigureAwait(false);
                    result = await Context.SaveChangesAsync().ConfigureAwait(false);

                    if(result < 1)
                    {
                        await transaction.RollbackAsync().ConfigureAwait(false);
                        return -1;
                    }

                    for(int i=0;i<PatrolLogList.Count;i++)
                    {
                        PatrolLogList[i].SPid = PatrolTB.Pid;
                    }

                    await Context.Patrollogtblists.AddRangeAsync(PatrolLogList).ConfigureAwait(false);
                    result = await Context.SaveChangesAsync().ConfigureAwait(false);

                    if(result < 1)
                    {
                        await transaction.RollbackAsync().ConfigureAwait(false);
                        return -1;
                    }

                    await transaction.CommitAsync().ConfigureAwait(false);
                    return 1;
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
