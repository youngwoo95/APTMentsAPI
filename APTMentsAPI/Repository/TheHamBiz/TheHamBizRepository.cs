using APTMentsAPI.DBModels;
using APTMentsAPI.Services.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;

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


                    IoParkingViewTb? ViewTB;
                    /*
                        VIEW TB SELECT 
                            ->
                        있으면 Update or 없으면 INSERT
                            ->
                        HistoryTB는 들어오는대로 INSERT
                    */
                    ViewTB = await Context.IoParkingViewTbs.FirstOrDefaultAsync(m => m.IoSeq == HistoryTB.IoSeq);
                    if(ViewTB is null)
                    {
                        // 없으면 INSERT
                        ViewTB = new IoParkingViewTb
                        {
                            IoSeq = HistoryTB.IoSeq, // 입출차 일련번호
                            ParkId = HistoryTB.ParkId, // 주차장ID
                            CarNum = HistoryTB.CarNum, // 차량번호
                            IoStatusTp = HistoryTB.IoStatusTp, // 입출 상태
                            IoStatusTpNm = HistoryTB.IoStatusTpNm, // 입출 상태명
                            InGateId = HistoryTB.IoGateId, // 입차 게이트 ID
                            InGateNm = HistoryTB.IoGateNm, // 입차 게이트 명
                            InLineNum = HistoryTB.IoLineNum, // 입차 라인 번호
                            InDtm = HistoryTB.IoDtm, // 입차시간
                            InLprStatus = HistoryTB.IoLprStatus, // 입차 LPR 상태
                            InLprStatusNm = HistoryTB.IoLprStatusNm, // 입차 LPR 상태 명칭
                            InTicketTp = HistoryTB.IoTicketTp, // 입차 차량 구분
                            InTicketTpNm = HistoryTB.IoTicketTpNm, // 입차 차량 구분 명
                            Dong = HistoryTB.Dong, // 동
                            Ho = HistoryTB.Ho, // 호
                            IsBlackList = HistoryTB.IsBlackList, // 블랙리스트 여부
                            BlackListReason = HistoryTB.BlackListReason, // 블랙리스트 사유
                            RegDtm = HistoryTB.RegDtm, // 블랙리스트 등록일시
                            InImagePath = HistoryTB.ImgPath, // 입차 사진
                            IsWait = HistoryTB.IsWait, // 입차처리할건지 대기할건지
                            IsWaitReason = HistoryTB.IsWaitReason, // 대기 걸린 차량의 이유
                            Etc = HistoryTB.Etc, // ETC
                            InCreateDt = HistoryTB.CreateDt, // 시스템 시간
                        };

                        await Context.IoParkingViewTbs.AddAsync(ViewTB).ConfigureAwait(false);
                        result = await Context.SaveChangesAsync();
                        if (result < 1)
                        {
                            await transaction.RollbackAsync();
                            return 0;
                        }
                    }
                    else
                    {
                        // 있으면 UPDATE - 입차 시퀀스번호 빼고 UPDATE
                        ViewTB.ParkId = HistoryTB.ParkId; // 주차장 ID
                        ViewTB.CarNum = HistoryTB.CarNum; // 차량번호
                        ViewTB.IoStatusTp = HistoryTB.IoStatusTp; // 입출 상태
                        ViewTB.IoStatusTpNm = HistoryTB.IoStatusTpNm; // 입출 상태명
                        ViewTB.InGateId = HistoryTB.IoGateId; // 입차 게이트 ID
                        ViewTB.InGateNm = HistoryTB.IoGateNm; // 입차 게이트 명
                        ViewTB.InLineNum = HistoryTB.IoLineNum; // 입차 라인 번호
                        ViewTB.InDtm = HistoryTB.IoDtm; // 입차시간
                        ViewTB.InLprStatus = HistoryTB.IoLprStatus; // 입차 LPR 상태
                        ViewTB.InLprStatusNm = HistoryTB.IoLprStatusNm; // 입차 LPR 상태 명칭
                        ViewTB.InTicketTp = HistoryTB.IoTicketTp; // 입차 차량 구분
                        ViewTB.InTicketTpNm = HistoryTB.IoTicketTpNm; // 입차 차량 구분 명
                        ViewTB.Dong = HistoryTB.Dong; // 동
                        ViewTB.Ho = HistoryTB.Ho; // 호
                        ViewTB.IsBlackList = HistoryTB.IsBlackList; // 블랙리스트 여부
                        ViewTB.BlackListReason = HistoryTB.BlackListReason; // 블랙리스트 사유
                        ViewTB.RegDtm = HistoryTB.RegDtm; // 블랙리스트 등록일시
                        ViewTB.InImagePath = HistoryTB.ImgPath; // 입차 사진
                        ViewTB.IsWait = HistoryTB.IsWait; // 입차처리할건지 대기할건지
                        ViewTB.IsWaitReason = HistoryTB.IsWaitReason; // 대기 걸린 차량의 이유
                        ViewTB.Etc = HistoryTB.Etc; // ETC
                        ViewTB.InCreateDt = HistoryTB.CreateDt; // 시스템 시간
                        Context.IoParkingViewTbs.Update(ViewTB);
                        result = await Context.SaveChangesAsync();
                        if(result < 1)
                        {
                            await transaction.RollbackAsync();
                            return 0;
                        }
                    }

                    // 외래키를 History에 넣어야함.
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

        /// <summary>
        /// 출차 등록
        /// </summary>
        /// <param name="HistoryTB"></param>
        /// <returns></returns>
        public async Task<int> AddOutCar(IoParkingHistory HistoryTB) 
        {
            try
            {
                using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    int result = 0;


                    IoParkingViewTb? ViewTB;

                    ViewTB = await Context.IoParkingViewTbs.FirstOrDefaultAsync(m => m.IoSeq == HistoryTB.IoSeq);
                    if(ViewTB is null)
                    {
                        // 없으면 INSERT
                        ViewTB = new IoParkingViewTb
                        {
                            IoSeq = HistoryTB.IoSeq, // 입출차 일련번호
                            OutTicketTp = HistoryTB.IoTicketTp, // 출차 차량 구분
                            OutTicketTpNm = HistoryTB.IoTicketTpNm, // 출차 차량 구분 명
                            IoStatusTp = HistoryTB.IoStatusTp, // 입출 상태
                            IoStatusTpNm = HistoryTB.IoStatusTpNm, // 입출 상태명
                            CarNum = HistoryTB.CarNum, // 차량 번호
                            //OutDtm = HistoryTB.IoDtm, // 출차 시간
                            ParkId = HistoryTB.ParkId, // 주차장ID
                            OutGateId = HistoryTB.IoGateId, // 출차 게이트 ID



                        };

                    }
                    else
                    {
                        // 있으면 UPDATE
                    }

                }


                    Console.WriteLine("");

                return 0;
            }catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

    }
}
