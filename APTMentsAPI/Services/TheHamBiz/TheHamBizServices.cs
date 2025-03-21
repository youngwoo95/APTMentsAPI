using APTMentsAPI.DBModels;
using APTMentsAPI.DTO;
using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;
using APTMentsAPI.Repository.TheHamBiz;
using APTMentsAPI.Services.FileService;
using APTMentsAPI.Services.Logger;

namespace APTMentsAPI.Services.TheHamBizService
{
    public class TheHamBizServices : ITheHamBizServices
    {
        private readonly ILoggerService LoggerService;
        private readonly ITheHamBizRepository TheHamBizRepository;
        private readonly IFileService FileService;

        public TheHamBizServices(ILoggerService _loggerservice,
            ITheHamBizRepository _thehambizrepository,
            IFileService _fileservice)
        {
            this.LoggerService = _loggerservice;
            this.TheHamBizRepository = _thehambizrepository;
            this.FileService = _fileservice;
        }

        /// <summary>
        /// 입차 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<int> AddInCarSerivce(RequestInTheHamBizDTO dto)
        {
            try
            {
                if (dto.IO_SEQ is null ||
                    dto.PARK_ID is null || 
                    dto.CAR_NUM is null ||
                    dto.IO_STATUS_TP is null ||
                    dto.IO_STATUS_TP_NM is null ||
                    dto.IN_GATE_ID is null ||
                    dto.IN_GATE_NM is null ||
                    dto.IN_DTM is null ||
                    dto.IN_TICKET_TP is null ||
                    dto.IN_TICKET_TP_NM is null ||
                    dto.IMG_PATH is null)
                    return 2; // 필수값 누락

                var ParkingRowTB = new IoParkingrow
                {
                    IoGubun = 1, // IO 구분 * 0: 출차 1: 입차 [NOT NULL]
                    IoSeq = dto.IO_SEQ, // 입출차 일련번호 [NOT NULL]
                    ParkId = dto.PARK_ID, // 주차장 ID [NOT NULL]
                    CarNum = dto.CAR_NUM, // 차량 번호 [NOT NULL]
                    IoStatusTp = dto.IO_STATUS_TP, // 입출 상태 [NOT NULL]
                    IoStatusTpNm = dto.IO_STATUS_TP_NM, // 입출 상태명 [NOT NULL]
                    IoGateId = dto.IN_GATE_ID, // 입차 GATE ID [NOT NULL]
                    IoGateNm = dto.IN_GATE_NM, // 입차 GATE NM [NOT NULL]
                    IoLineNum = dto.IN_LINE_NUM, // 입차 라인 번호 [NOT NULL]
                    IoDtm = DateTime.Parse(dto.IN_DTM), // 입차 일시 [NOT NULL]
                    IoLprStatus = dto.IN_LPR_STATUS, // 입차 LPR 상태 ID
                    IoLprStatusNm = dto.IN_LPR_STATUS_NM, // 입차 LPR 상태 명칭
                    IoTicketTp = dto.IN_TICKET_TP, // 입차 차량 구분 ID [NOT NULL]
                    IoTicketTpNm = dto.IN_TICKET_TP_NM, // 입차 차량 구분 [NOT NULL]
                    Dong = dto.DONG, // 동
                    Ho = dto.HO, // 호
                    IsReservation = dto.IS_RESERVATION, // 예약차량여부
                    IsBlackList = dto.BLACK_LIST_INFO?.IS_BLACK_LIST, // 블랙리스트 여부
                    BlackListReason = dto.BLACK_LIST_INFO?.BLACK_LIST_REASON, // 블랙리스트 사유
                    RegDtm = dto.BLACK_LIST_INFO?.REG_DTM, // 블랙리스트 등록일자 처리 // 블랙리스트 등록일자
                    ImgPath = dto.IMG_PATH, // 이미지경로 [NOT NULL]
                    IsWait = dto.IS_WAIT, // 입차 처리/대기 여부 (0, 1, 2) [NOT NULL]
                    IsWaitReason = dto.IS_WAIT_REASON, // 대기 걸린 차량의 이유
                    Etc = dto.ETC, // 기타 정보
                    CreateDt = DateTime.Now
                };

                // IO_SEQ 검사 -->
                // IS_WAIT == 1
                /*
                 정기차량(입주민) 0으로 전송
                - 예약차량 자동입차 현장인경우: 0으로 전송

                방문차량, 블랙리스트 1로 전송후 입차시 2로 전송
                예약차량 현장옵션에 따라 틀립니다.
                - 예약차량인데 방문증을 뽑고 들어가는 현장: 1로 전송 후 2번 전송
                 */
               
                int result = await TheHamBizRepository.AddInCarAsnyc(ParkingRowTB).ConfigureAwait(false);
                if (result > 0)
                    return 1;
                else if (result == 0)
                    return 0;
                else
                    return -1;
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 출차 코드
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<int> AddOutCarService(RequestOutTheHamBizDTO dto)
        {
            try
            {
                if (dto.IO_SEQ is null || // 입출차 일련번호
                    dto.PARK_ID is null ||  // 주차장 ID
                    dto.CAR_NUM is null ||  // 차량 번호
                    dto.IO_STATUS_TP is null || // 입출 상태
                    dto.IO_STATUS_TP_NM is null || // 입출 상태 명
                    dto.OUT_GATE_ID is null || // 출차 GATE ID
                    dto.OUT_GATE_NM is null || // 출차 GATE NM
                    dto.OUT_DTM is null || // 출차 일시
                    dto.OUT_TICKET_TP is null || // 출차 차량 구분
                    dto.OUT_TICKET_TP_NM is null || // 출차 차량 구분 명
                    dto.IMG_PATH is null) // 이미지 경로
                        return 2; // 필수값 누락

                var ParkingRowTB = new IoParkingrow();

                ParkingRowTB.IoGubun = 0; // IO 구분 * 0: 출차 1: 입차 [NOT NULL]
                ParkingRowTB.IoSeq = dto.IO_SEQ; // 입출차 일련번호 [NOT NULL]
                ParkingRowTB.ParkId = dto.PARK_ID; // 주차장 ID [NOT NULL]
                ParkingRowTB.CarNum = dto.CAR_NUM; // 차량 번호 [NOT NULL]
                ParkingRowTB.IoStatusTp = dto.IO_STATUS_TP; // 입출 상태 [NOT NULL]
                ParkingRowTB.IoStatusTpNm = dto.IO_STATUS_TP_NM; // 입출 상태명 [NOT NULL]
                ParkingRowTB.IoGateId = dto.OUT_GATE_ID; // 입차 GATE ID [NOT NULL]
                ParkingRowTB.IoGateNm = dto.OUT_GATE_NM; // 입차 GATE NM [NOT NULL]
                ParkingRowTB.IoLineNum = dto.OUT_LINE_NUM; // 입차 라인 번호 [NOT NULL]
                ParkingRowTB.IoDtm = DateTime.Parse(dto.OUT_DTM); // 입차 일시 [NOT NULL]
                ParkingRowTB.IoLprStatus = dto.OUT_LPR_STATUS; // 입차 LPR 상태 ID
                ParkingRowTB.IoLprStatusNm = dto.OUT_LPR_STATUS_NM; // 입차 LPR 상태 명칭
                ParkingRowTB.IoTicketTp = dto.OUT_TICKET_TP; // 입차 차량 구분 ID [NOT NULL]
                ParkingRowTB.IoTicketTpNm = dto.OUT_TICKET_TP_NM; // 입차 차량 구분 [NOT NULL]
                ParkingRowTB.Dong = dto.DONG; // 동
                ParkingRowTB.Ho = dto.HO; // 호
                ParkingRowTB.IsReservation = dto.IS_RESERVATION; // 예약차량여부

                if (dto.BLACK_LIST_INFO is not null)
                {
                    ParkingRowTB.IsBlackList = dto.BLACK_LIST_INFO.IS_BLACK_LIST; // 블랙리스트 여부
                    ParkingRowTB.BlackListReason = dto.BLACK_LIST_INFO.BLACK_LIST_REASON; // 블랙리스트 사유
                    ParkingRowTB.RegDtm = dto.BLACK_LIST_INFO.REG_DTM; // 블랙리스트 등록일자 처리 // 블랙리스트 등록일자
                }
                else
                {
                    ParkingRowTB.IsBlackList = "0";
                    ParkingRowTB.BlackListReason = null;
                    ParkingRowTB.RegDtm = null;
                }

                ParkingRowTB.ImgPath = dto.IMG_PATH; // 이미지경로 [NOT NULL]
                ParkingRowTB.Etc = dto.ETC; // 기타 정보
                ParkingRowTB.CreateDt = DateTime.Now;
                ParkingRowTB.ParkDuration = dto.PARK_DURATION; // 주차 시간
                ParkingRowTB.VisitTime = dto.VISIT_TIME; // 방문 시간

                int result = await TheHamBizRepository.AddOutCarAsync(ParkingRowTB).ConfigureAwait(false);
                if (result > 0)
                    return 1;
                else if (result == 0)
                    return 0;
                else
                    return -1;
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 순찰 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<int> AddPatrolService(RequestPadTheHamBizDTO dto)
        {
            try
            {
                if (dto.PARK_ID is null ||
                    dto.PATROL_USER_NM is null ||
                    dto.PATROL_START_DTM is null ||
                    dto.PATROL_END_DTM is null ||
                    dto.PAY_LOAD is null)
                    return 2; // 필수값 누락

                foreach(var requiredCheck in dto.PAY_LOAD)
                {
                    if (requiredCheck.PATROL_DTM is null || requiredCheck.CAR_NUM is null)
                        return 2; // 필수값 누락
                }

                DateTime NowDate = DateTime.Now;

                int loopCount = dto.PAY_LOAD.Count;

                List<Patrolpadlogtb> modellist = new List<Patrolpadlogtb>();

                for (int i = 0; i < dto.PAY_LOAD.Count; i++)
                {
                    var model = new Patrolpadlogtb()
                    {
                        ParkId = dto.PARK_ID, // (필수값) 주차장 ID
                        PatrolUserId = dto.PATROL_USER_ID, // (필수값) 순찰 담당자 ID
                        PatrolUserNm = dto.PATROL_USER_NM, // (필수값) 순찰 담당자 이름
                        PatrolStartDtm = DateTime.Parse(dto.PATROL_START_DTM), // (필수값) 순찰 시작 일시
                        PatrolEndDtm = DateTime.Parse(dto.PATROL_END_DTM), // (필수값) 순찰 종료 일시
                        TotCnt = dto.TOT_CNT, // 전체 데이터 개수
                        PatrolDtm = DateTime.Parse(dto.PAY_LOAD[i].PATROL_DTM!), // (필수값) 순찰 일시
                        PatrolCode = dto.PAY_LOAD[i].PATROL_CODE, // (필수값) 순찰 상태 코드 0: 정상(입주민), 1: 방문객, 2: 순찰, 3: 위반(블랙리스트)
                        PatrolName = dto.PAY_LOAD[i].PATROL_NAME, // 순찰 상태 명
                        CarNum = dto.PAY_LOAD[i].CAR_NUM!, //  (필수값) 차량 번호
                        PatrolImg = dto.PAY_LOAD[i].PATROL_IMG, // 순찰 이미지
                        PatrolRemark = dto.PAY_LOAD[i].PATROL_REMARK, // 순찰비고
                    };
                    modellist.Add(model);
                }


                int result = await TheHamBizRepository.AddPatrolAsync(modellist).ConfigureAwait(false);
                if (result > 0)
                    return 1;
                else if (result == 0)
                    return 0;
                else
                    return -1;
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 입-출차 리스트
        /// </summary>
        /// <returns></returns>
        public async Task<ResponsePage<PageNationDTO<InOutViewListDTO>>?> InOutViewListService(int pageNumber, int PageSize, DateTime? StartDate, DateTime? EndDate, string? inStatusTpNm, string? CarNumber, string? Dong, string? Ho, int? PackingDuration, string? ioTicketTpNm)
        {
            try
            {
                if (pageNumber == 0)
                    return new ResponsePage<PageNationDTO<InOutViewListDTO>>() {data = null, code = 200 };
                if (PageSize == 0)
                    return new ResponsePage<PageNationDTO<InOutViewListDTO>>() {data = null, code = 200 };

                var model = await TheHamBizRepository.InOutViewListAsync(pageNumber, PageSize, StartDate, EndDate, inStatusTpNm, CarNumber, Dong, Ho, PackingDuration, ioTicketTpNm).ConfigureAwait(false);
                if (model is not null)
                    return model;
                else
                    return new ResponsePage<PageNationDTO<InOutViewListDTO>>() { data = null, code = 400 };
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponsePage<PageNationDTO<InOutViewListDTO>>() { data = null, code = 500 };
            }
        }

        /// <summary>
        /// 시퀀스 상세 내역 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        public async Task<ResponsePage<List<DetailViewDTO>>?> DetailViewService(string ioSeq)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(ioSeq))
                    return new ResponsePage<List<DetailViewDTO>>() {data = null, code = 200 };

                var model = await TheHamBizRepository.DetailViewListAsync(ioSeq).ConfigureAwait(false);
                if (model is not null)
                {
                    var detailViewTasks = model.Select(async item => new DetailViewDTO
                    {
                        pId = item.Pid,
                        ioGubun = item.IoGubun,
                        ioSeq = item.IoSeq,
                        parkId = item.ParkId,
                        carNum = item.CarNum,
                        ioStatusTp = item.IoStatusTp,
                        ioStatusTpNm = item.IoStatusTpNm,
                        ioGateId = item.IoGateId,
                        ioGateNm = item.IoGateNm,
                        ioLineNum = item.IoLineNum,
                        ioDtm = item.IoDtm,
                        ioLprStatus = item.IoLprStatus,
                        ioLprStatusNm = item.IoLprStatusNm,
                        ioTicketTp = item.IoTicketTp,
                        ioTicketTpNm = item.IoTicketTpNm,
                        dong = item.Dong,
                        ho = item.Ho,
                        isReservation = item.IsReservation,
                        isBlacklist = item.IsBlackList,
                        blacklistReason = item.BlackListReason,
                        regDtm = item.RegDtm,
                        imgPath = item.ImgPath,
                        //imgPath = await FileService.GetImageFile(item.ImgPath,"InOutImages"),
                        isWait = item.IsWait,
                        isWaitReason = item.IsWaitReason,
                        parkDuration = item.ParkDuration,
                        visitTime = item.VisitTime,
                        etc = item.Etc,
                        memo = item.Memo
                    }).ToList();

                    // 모든 Task를 await 하고 결과를 리스트로 변환
                    var detailViewList = (await Task.WhenAll(detailViewTasks)).ToList();

                    return new ResponsePage<List<DetailViewDTO>>()
                    {
                        data = detailViewList,
                        code = 200
                    };
                }
                else
                    return new ResponsePage<List<DetailViewDTO>>() {data = null, code = 200 };
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponsePage<List<DetailViewDTO>>() {data = null, code = 500 };
            }
        }

        /// <summary>
        /// 해당 차량 최근 7일치 조회
        /// </summary>
        /// <param name="carNum"></param>
        /// <returns></returns>
        public async Task<ResponsePage<List<LastWeeksDTO>>?> LastWeeksService(string carNum)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(carNum))
                    return new ResponsePage<List<LastWeeksDTO>>() { data = null, code = 200 };
                //return new ResponseList<LastWeeksDTO>() { message = "잘못된 요청입니다.", data = null, code = 200 };

                DateTime Today = DateTime.Now;
                var model = await TheHamBizRepository.LastWeeksListAsync(carNum,Today).ConfigureAwait(false);
                if (model is not null)
                {
                    var LastViewList = model.Select(item => new LastWeeksDTO
                    {
                        pId = item.Pid,
                        ioGubun = item.IoGubun,
                        ioSeq = item.IoSeq,
                        parkId = item.ParkId,
                        carNum = item.CarNum,
                        ioStatusTp = item.IoStatusTp,
                        ioStatusTpNm = item.IoStatusTpNm,
                        ioGateId = item.IoGateId,
                        ioGateNm = item.IoGateNm,
                        ioLineNum = item.IoLineNum,
                        ioDtm = item.IoDtm,
                        ioLprStatus = item.IoLprStatus,
                        ioLprStatusNm = item.IoLprStatusNm,
                        ioTicketTp = item.IoTicketTp,
                        ioTicketTpNm = item.IoTicketTpNm,
                        dong = item.Dong,
                        ho = item.Ho,
                        isReservation = item.IsReservation,
                        isBlacklist = item.IsBlackList,
                        blacklistReason = item.BlackListReason,
                        regDtm = item.RegDtm,
                        imgPath = item.ImgPath,
                        isWait = item.IsWait,
                        isWaitReason = item.IsWaitReason,
                        parkDuration = item.ParkDuration,
                        visitTime = item.VisitTime,
                        etc = item.Etc,
                        memo = item.Memo
                    }).ToList();

                    return new ResponsePage<List<LastWeeksDTO>>() { data = LastViewList, code = 200 };
                }
                else
                    return new ResponsePage<List<LastWeeksDTO>>() { data = null, code = 200 };
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponsePage<List<LastWeeksDTO>>() {data = null, code = 500 };
            }
        }

        /// <summary>
        /// View 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseUnit<bool>> UpdateViewMemoService(UpdateMemoDTO dto)
        {
            try
            {
                var model = await TheHamBizRepository.UpdateViewMemoAsync(dto);
                if(model > 0)
                {
                    return new ResponseUnit<bool>() {data = true, code = 200 };
                }
                else if(model == 0)
                {
                    return new ResponseUnit<bool>() { data = false, code = 404 };
                }
                else
                {
                    return new ResponseUnit<bool>() {  data = false, code = 500 };
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponseUnit<bool>() {  data = false, code = 500 };
            }
        }

        /// <summary>
        /// Row 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseUnit<bool>> UpdateRowsMemoService(UpdateMemoDTO dto)
        {
            try
            {
                var model = await TheHamBizRepository.UpdateRowMemoAsync(dto).ConfigureAwait(false);
                if (model > 0)
                {
                    return new ResponseUnit<bool>() {  data = true, code = 200 };
                }
                else if (model == 0)
                {
                    return new ResponseUnit<bool>() { data = false, code = 404 };
                }
                else
                {
                    return new ResponseUnit<bool>() {  data = false, code = 500 };
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponseUnit<bool>() {  data = false, code = 500 };
            }
        }

        /// <summary>
        /// 순찰 리스트 조회
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<ResponsePage<PageNationDTO<PatrolViewListDTO>>?> PatrolViewListService(int pageNumber, int PageSize, DateTime? startDate, DateTime? endDate, string? patrolNm, string? carNumber)
        {
            try
            {
                if (pageNumber == 0)
                    return new ResponsePage<PageNationDTO<PatrolViewListDTO>>() {  data = null, code = 200 };
                if (PageSize == 0)
                    return new ResponsePage<PageNationDTO<PatrolViewListDTO>>() {  data = null, code = 200 };

                var model = await TheHamBizRepository.PatrolViewListAsync(pageNumber, PageSize, startDate, endDate, patrolNm, carNumber);
                if (model is null)
                    return new ResponsePage<PageNationDTO<PatrolViewListDTO>>() { data = null, code = 400 };
                else
                    return model;
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponsePage<PageNationDTO<PatrolViewListDTO>>() { data = null, code = 500 };
            }
        }

   
    }
}
