using APTMentsAPI.DBModels;
using APTMentsAPI.DTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;
using APTMentsAPI.Services.FileService;
using APTMentsAPI.Services.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APTMentsAPI.Repository.TheHamBiz
{
    public class TheHamBizRepository : ITheHamBizRepository
    {
        private readonly ILoggerService LoggerService;
        private readonly IFileService FileService;
        private readonly AptContext Context;

        public TheHamBizRepository(AptContext _context,
            ILoggerService _loggerservice,
            IFileService _fileservice)
        {
            this.Context = _context;
            this.LoggerService = _loggerservice;
            this.FileService = _fileservice;
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
                                InStatusTp = RowsTB.IoStatusTp, // 입출 상태
                                InStatusTpNm = RowsTB.IoStatusTpNm, // 입출 상태명
                                IoTicketTp = RowsTB.IoTicketTp,
                                IoTicketTpNm = RowsTB.IoTicketTpNm,
                                InPid = RowsTB.Pid, // 최종 입차 INDEX
                                InDtm = RowsTB.IoDtm, // 입차 시간
                                CarNum = RowsTB.CarNum, // 차량 번호
                                Dong = RowsTB.Dong, // 동
                                Ho = RowsTB.Ho, // 호
                                IsBlackList = RowsTB.IsBlackList, // 블랙 리스트 여부
                                BlackListReason = RowsTB.BlackListReason, // 블랙 리스트 사유
                                UpdateDt = RowsTB.CreateDt
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
                            ViewTableCheck.InStatusTp = RowsTB.IoStatusTp; // 입출 상태
                            ViewTableCheck.InStatusTpNm = RowsTB.IoStatusTpNm; // 입출 상태명
                            ViewTableCheck.IoTicketTp = RowsTB.IoTicketTp;
                            ViewTableCheck.IoTicketTpNm = RowsTB.IoTicketTpNm;
                            ViewTableCheck.InPid = RowsTB.Pid; // 최종 입차 INDEX
                            ViewTableCheck.InDtm = RowsTB.IoDtm; // 최종 입차 시간
                            ViewTableCheck.CarNum = RowsTB.CarNum; // 차량 번호
                            ViewTableCheck.Dong = RowsTB.Dong; // 동
                            ViewTableCheck.Ho = RowsTB.Ho; // 호
                            ViewTableCheck.IsBlackList = RowsTB.IsBlackList; // 블랙 리스트 여부
                            ViewTableCheck.BlackListReason = RowsTB.BlackListReason; // 블랙리스트 사유
                            ViewTableCheck.UpdateDt = RowsTB.CreateDt;

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
                    // 여기서 파일 저장해야할것같네.
                    /*
                    bool? fileSave = await FileService.AddImageFile(RowsTB.ImgPath, "InOutImages");
                    if (fileSave == true)
                    {
                        await transaction.CommitAsync().ConfigureAwait(false);
                        return 1;
                    }
                    else
                    {
                        await transaction.RollbackAsync().ConfigureAwait(false);
                        return 0;
                    }
                    */
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
                            InStatusTp = RowsTB.IoStatusTp, // 입출 상태
                            InStatusTpNm = RowsTB.IoStatusTpNm, // 입출 상태 명
                            IoTicketTp = RowsTB.IoTicketTp,
                            IoTicketTpNm = RowsTB.IoTicketTpNm,
                            OutPid = RowsTB.Pid, // 최종 출차 INDEX
                            OutDtm = RowsTB.IoDtm, // 출차 시간
                            CarNum = RowsTB.CarNum, // 차량 번호
                            Dong = RowsTB.Dong, // 동
                            Ho = RowsTB.Ho,
                            ParingDuration = RowsTB.ParkDuration, // 주차 시간
                            IsBlackList = RowsTB.IsBlackList, // 블랙리스트 여부
                            BlackListReason = RowsTB.BlackListReason, // 블랙리스트 사유
                            UpdateDt = RowsTB.CreateDt,
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
                        ViewTableCheck.InStatusTp = RowsTB.IoStatusTp; // 입출 상태
                        ViewTableCheck.InStatusTpNm = RowsTB.IoStatusTpNm; // 입출 상태명
                        ViewTableCheck.IoTicketTp = RowsTB.IoTicketTp;
                        ViewTableCheck.IoTicketTpNm = RowsTB.IoTicketTpNm;
                        ViewTableCheck.OutPid = RowsTB.Pid; // 최종 출차 INDEX
                        ViewTableCheck.OutDtm = RowsTB.IoDtm;// 최종 출차 시간
                        ViewTableCheck.CarNum = RowsTB.CarNum; // 차량 번호
                        ViewTableCheck.Dong = RowsTB.Dong; // 동
                        ViewTableCheck.Ho = RowsTB.Ho; // 호
                        ViewTableCheck.ParingDuration = RowsTB.ParkDuration; // 주차시간
                        ViewTableCheck.IsBlackList = RowsTB.IsBlackList; // 블랙리스트 여부
                        ViewTableCheck.BlackListReason = RowsTB.BlackListReason; // 블랙리스트 사유
                        ViewTableCheck.UpdateDt = RowsTB.CreateDt; // 시스템 생성일자

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
                    // 여기서 파일 저장해야할것같네.
                    /*
                    bool? fileSave = await FileService.AddImageFile(RowsTB.ImgPath, "InOutImages");
                    if (fileSave == true)
                    {
                        await transaction.CommitAsync().ConfigureAwait(false);
                        return 1;
                    }
                    else
                    {
                        await transaction.RollbackAsync().ConfigureAwait(false);
                        return 0;
                    }
                    */
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

                    // 여기왔을때 PatrolLogList의 Image들 저장해야함.
                    /*
                    foreach(var item in PatrolLogList)
                    {
                        //item.PatrolImg
                        if (!String.IsNullOrWhiteSpace(item.PatrolImg)) 
                        {
                            bool fileSave = await FileService.AddImageFile(item.PatrolImg, "PatrolImages");
                            if (!fileSave)
                                return 0;
                        }
                    }
                    */
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
        /// 입-출차 리스트 조회
        /// </summary>
        /// <returns></returns>
        public async Task<ResponsePage<PageNationDTO<InOutViewListDTO>>?> InOutViewListAsync(int pageNumber, int pageSize, DateTime? startDate, DateTime? EndDate, string? inStatusTpNm, string? CarNumber, string? Dong, string? Ho, int? ParkingDuration, string? ioTicketTpNm)
        {
            try
            {
                //int pageNumber = 1; // 첫번째 페이지
                //int pageSize = 15; // 개수
                // 1. 전체 데이터 개수를 조회합니다.
                int totalCount = await Context.IoParkingviewtbs.CountAsync();

                var query = Context.IoParkingviewtbs
                    .Include(v => v.InP)
                    .Include(v => v.OutP)
                    .AsQueryable();

                if(inStatusTpNm == null) // 전체
                {
                    if (startDate.HasValue)
                    {
                        query = query.Where(m => m.InDtm >= startDate.Value.Date); // 입차 ~ 출차 (전체)
                    }

                    if (EndDate.HasValue)
                    {
                        query = query.Where(m => m.OutDtm < EndDate.Value.Date.AddDays(1));
                    }
                }
                else if(inStatusTpNm == "입차")
                {
                    query = query.Where(m => m.InStatusTpNm == "입차"); // 입차
                    
                    if (startDate.HasValue)
                    {
                        query = query.Where(m => m.InDtm >= startDate.Value.Date); // 입차시간이 ㅇㅇ 날 ~ ㅇㅇ 날
                    }

                    if (EndDate.HasValue)
                    {
                        query = query.Where(m => m.InDtm < EndDate.Value.Date.AddDays(1));
                    }
                }
                else if(inStatusTpNm == "출차")
                {
                    query = query.Where(m => m.InStatusTpNm == "출차"); // 출차
                    if (startDate.HasValue)
                    {
                        query = query.Where(m => m.OutDtm >= startDate.Value.Date); // 출차시간이 ㅇㅇ 날 ~ ㅇㅇ 날
                    }

                    if (EndDate.HasValue)
                    {
                        query = query.Where(m => m.OutDtm < EndDate.Value.Date.AddDays(1));
                    }
                }

                if (CarNumber is not null)
                {
                    query = query.Where(m => m.CarNum == CarNumber);
                }

                if(Dong is not null)
                {
                    query = query.Where(m => m.Dong == Dong);
                }

                if(Ho is not null)
                {
                    query = query.Where(m => m.Ho == Ho);
                }

                if (ParkingDuration.HasValue)
                {
                    query = query.Where(m => m.ParingDuration == ParkingDuration);
                }

                if(ioTicketTpNm is not null)
                {
                    query = query.Where(m => m.IoTicketTpNm == ioTicketTpNm);
                }

                var pageView = await query
                .OrderBy(x => x.Pid)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

                var detailViewTasks = pageView.Select(async item => new InOutViewListDTO
                {
                    pId = item.Pid,
                    ioSeq = item.IoSeq,
                    ioTicketTp = item.IoTicketTp,
                    ioTicketTpNm = item.IoTicketTpNm,
                    //ioTicketTp = item.OutP == null ? item.InP.IoTicketTp : item.OutP.IoTicketTp,
                    //ioTicketTpNm = item.OutP == null ? item.InP.IoTicketTpNm : item.OutP.IoTicketTpNm,
                    ioStatusTp = item.InStatusTp,
                    ioStatusTpNm = item.InStatusTpNm,
                    //ioStatusTp = item.OutP == null ? item.InP.IoStatusTp : item.OutP.IoStatusTp,
                    //ioStatusTpNm = item.OutP == null ? item.InP.IoStatusTpNm : item.OutP.IoStatusTpNm,
                    carNum = item.CarNum,
                    //inDtm = item.InP.IoDtm,
                    inDtm = item.InDtm,
                    //outDtm = item.OutP?.IoDtm, // OutP가 없으면 null로 처리
                    outDtm = item.OutDtm,
                    parkingDuration = item.OutP?.ParkDuration ?? 0, // null이면 0 처리 (필요에 따라 조정)
                    inGateId = item.InP?.IoGateId,
                    inGateNm = item.InP?.IoGateNm,
                    outGateId = item.OutP?.IoGateId,
                    outGateNm = item.OutP?.IoGateNm,
                    dong = item.Dong,
                    ho = item.Ho,
                    inImagePath = item.InP?.ImgPath ?? string.Empty,
                    outImagePath = item.OutP?.ImgPath ?? string.Empty,
                    //inImagePath = await FileService.GetImageFile(item.InP?.ImgPath ?? string.Empty, "InOutImages"),
                    //outImagePath = await FileService.GetImageFile(item.OutP?.ImgPath ?? string.Empty, "InOutImages"),
                    isBlacklist = item.IsBlackList,
                    blacklistReason = item.BlackListReason,
                    memo = item.Memo
                }).ToList();

                var model = (await Task.WhenAll(detailViewTasks)).ToList();

                if (model is not null)
                {

                    // ResponseList에 페이지네이션 DTO를 할당합니다.
                    var result = new ResponsePage<PageNationDTO<InOutViewListDTO>>
                    {
                        // Metas는 별도의 페이지 정보가 필요하다면 사용(중복되는 정보일 수 있으므로 필요에 따라 제거)
                        Metas = new Meta
                        {
                            pageNumber = pageNumber,
                            pageSize = pageSize,
                            totalCount = totalCount
                        },
                        data = new PageNationDTO<InOutViewListDTO>
                        {
                            Items = model
                        },
                        code = 200
                    };
                    return result;
                }
                else
                {
                    return new ResponsePage<PageNationDTO<InOutViewListDTO>>();
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 시퀀스 상세내역 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        public async Task<List<IoParkingrow>?> DetailViewListAsync(string ioSeq)
        {
            try
            {
                var model = await Context.IoParkingrows.Where(m => m.IoSeq == ioSeq).ToListAsync().ConfigureAwait(false);
                if (model is null)
                    return new List<IoParkingrow>();
                else
                    return model;
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 지난 최근 7일 조회
        /// </summary>
        /// <param name="carNum"></param>
        /// <param name="ThisTime"></param>
        /// <returns></returns>
      public async Task<List<IoParkingrow>?> LastWeeksListAsync(string carNum, DateTime ThisTime)
        {
            try
            {
                // ThisTime의 날짜 부분(자정)에서 7일 전 날짜를 구함.
                DateTime SearchDate = ThisTime.Date.AddDays(-7);

                // 조건에 맞는 IoParkingviewtb 데이터를 조회하며, 네비게이션 프로퍼티(InP, OutP)도 Include 함.
                var viewList = await Context.IoParkingviewtbs
                    .Where(m => m.CarNum == carNum && m.UpdateDt >= SearchDate)
                    .Include(m => m.InP)
                    .Include(m => m.OutP)
                    .ToListAsync()
                    .ConfigureAwait(false);

                // 조회된 데이터가 없으면 빈 리스트 반환.
                if (viewList == null || !viewList.Any())
                    return new List<IoParkingrow>();

                // InP와 OutP 데이터를 하나의 리스트에 합침.
                List<IoParkingrow> result = new List<IoParkingrow>();
                foreach (var view in viewList)
                {
                    if (view.InP != null)
                        result.Add(view.InP);
                    if (view.OutP != null)
                        result.Add(view.OutP);
                }
                result = result.OrderBy(m => m.CreateDt).ToList();
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// View 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<int> UpdateViewMemoAsync(UpdateMemoDTO dto)
        {
            try
            {
                using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    var model = await Context.IoParkingviewtbs.FirstOrDefaultAsync(m => m.Pid == dto.pId).ConfigureAwait(false);
                    if (model is not null)
                    {
                        model.Memo = dto.memo;
                        model.UpdateDt = DateTime.Now;

                        Context.IoParkingviewtbs.Update(model);
                        int result = await Context.SaveChangesAsync().ConfigureAwait(false);
                        if(result == 0)
                        {
                            await transaction.RollbackAsync().ConfigureAwait(false);
                            return -1;
                        }
                        else
                        {
                            await transaction.CommitAsync().ConfigureAwait(false);
                            return 1;
                        }
                    }
                    else
                        return 0;
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Row 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<int> UpdateRowMemoAsync(UpdateMemoDTO dto)
        {
            try
            {
                using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    var model = await Context.IoParkingrows.FirstOrDefaultAsync(m => m.Pid == dto.pId).ConfigureAwait(false);
                    if (model is not null)
                    {
                        model.Memo = dto.memo;
                        model.CreateDt = DateTime.Now;

                        Context.IoParkingrows.Update(model);
                        int result = await Context.SaveChangesAsync().ConfigureAwait(false);
                        if (result == 0)
                        {
                            await transaction.RollbackAsync().ConfigureAwait(false);
                            return -1;
                        }
                        else
                        {
                            await transaction.CommitAsync().ConfigureAwait(false);
                            return 1;
                        }
                    }
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 순찰 List 조회
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResponsePage<PageNationDTO<PatrolViewListDTO>>?> PatrolViewListAsync(int pageNumber, int pageSize)
        {
            try
            {
                int totalCount = await Context.Patrolpadlogtbs.CountAsync().ConfigureAwait(false);

                var query = Context.Patrolpadlogtbs
                    .Include(m => m.Patrollogtblists)
                    .AsQueryable();

                var pageView = await query
                    .OrderBy(m => m.Pid)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync()
                    .ConfigureAwait(false);


                List<PatrolViewListDTO> model = new List<PatrolViewListDTO>();
                foreach (var views in pageView)
                {
                    var item = new PatrolViewListDTO();
                    item.pId = views.Pid; // PID
                    item.parkId = views.ParkId; // 주차장ID
                    item.partolUserId = views.PatrolUserId; // 순찰 담당자ID
                    item.patrolUserNm = views.PatrolUserNm; // 순찰 담당자 이름
                    item.patrolStartDtm = views.PatrolStartDtm; // 순찰 시작 일시
                    item.patrolEndDtm = views.PatrolEndDtm; // 순찰 종료 일시
                    item.totCnt = views.TotCnt; // 전체 데이터 개수

                    foreach (var logs in views.Patrollogtblists)
                    {
                        var subitem = new PatrolLowList();
                        subitem.pId = logs.Pid; // PID
                        subitem.patrolDtm = logs.PatrolDtm; // 순찰 일시
                        subitem.patrolCode = logs.PatrolCode; // 순찰 상태 코드
                        subitem.patrolName = logs.PatrolName; // 순찰 상태명
                        subitem.carNum = logs.CarNum; // 차량 번호
                        subitem.patrolImg = logs.PatrolImg; // 순찰 이미지
                        /*
                        if (!String.IsNullOrWhiteSpace(logs.PatrolImg))
                        {
                            subitem.patrolImg = await FileService.GetImageFile(logs.PatrolImg, "PatrolImages");
                        }
                        else
                        {
                            subitem.patrolImg = null;
                        }
                        */
                        subitem.patrolRemark = logs.PatrolRemark; // 순찰 비고
                        item.lowList.Add(subitem);
                    }
                    model.Add(item);
                }

                if (model is not null)
                {
                    // PageNationDTO에 데이터를 채워서 반환
                    var result = new ResponsePage<PageNationDTO<PatrolViewListDTO>>
                    {
                        Metas = new Meta
                        {
                            pageNumber = pageNumber,
                            pageSize = pageSize,
                            totalCount = totalCount
                        },
                        data = new PageNationDTO<PatrolViewListDTO>
                        {
                            Items = model
                        },
                        code = 200
                    };
                    return result;
                }
                else
                    return new ResponsePage<PageNationDTO<PatrolViewListDTO>>();
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return null;
            }
        }
    }
}
