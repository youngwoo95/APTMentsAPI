using APTMentsAPI.DBModels;
using APTMentsAPI.DTO;
using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;
using APTMentsAPI.Repository.TheHamBiz;
using APTMentsAPI.Services.Logger;

namespace APTMentsAPI.Services.TheHamBizService
{
    public class TheHamBizServices : ITheHamBizServices
    {
        private readonly ILoggerService LoggerService;
        private readonly ITheHamBizRepository TheHamBizRepository;

        public TheHamBizServices(ILoggerService _loggerservice,
            ITheHamBizRepository _thehambizrepository)
        {
            this.LoggerService = _loggerservice;
            this.TheHamBizRepository = _thehambizrepository;
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
                    IsBlackList = dto.BLACK_LIST_INFO.IS_BLACK_LIST, // 블랙리스트 여부
                    BlackListReason = dto.BLACK_LIST_INFO.BLACK_LIST_REASON, // 블랙리스트 사유
                    RegDtm = dto.BLACK_LIST_INFO.REG_DTM, // 블랙리스트 등록일자 처리 // 블랙리스트 등록일자
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
               
            
                int result = await TheHamBizRepository.AddInCarAsnyc(ParkingRowTB);
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

                int result = await TheHamBizRepository.AddOutCarAsync(ParkingRowTB);
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
                    dto.PATROL_USER_ID is null ||
                    dto.PATROL_START_DTM is null ||
                    dto.PATROL_END_DTM is null ||
                    dto.PAY_LOAD is null)
                    return 2; // 필수값 누락

                foreach(var requiredCheck in dto.PAY_LOAD)
                {
                    if (requiredCheck.CAR_NUM is null)
                        return 2; // 필수값 누락
                }

                DateTime NowDate = DateTime.Now;

                var PatrolTB = new Patrolpadlogtb();
                PatrolTB.ParkId = dto.PARK_ID; // 주차장 ID  ex)샘플 데이터  "2177"
                PatrolTB.PatrolUserId = dto.PATROL_USER_ID; // 순찰 담당자 ID  ex)샘플 데이터  "33마 3434"
                PatrolTB.PatrolUserNm = dto.PATROL_USER_NM; // 순찰 담당자 이름 ex) "10"
                PatrolTB.PatrolStartDtm = dto.PATROL_START_DTM;// 순찰 시작 일시 ex)샘플 데이터  "입차"
                PatrolTB.PatrolEndDtm = dto.PATROL_END_DTM; // 순찰 종료 일시 ex)샘플 데이터  "1"
                PatrolTB.TotCnt = dto.TOT_CNT; // 전체 데이터 개수 ex) 샘플데이터 3
                PatrolTB.CreateDt = NowDate;

                List<Patrollogtblist> PatrolList = new List<Patrollogtblist>();
                foreach(var item in dto.PAY_LOAD)
                {
                    PatrolList.Add(new Patrollogtblist
                    {
                        PatrolDtm = Convert.ToDateTime(item.PATROL_DTM), // 순찰 일시 ex) 샘플데이터 "2025-11-22 22:22:22"
                        PatrolCode = item.PATROL_CODE, // 순찰 상태 코드 ex) 샘플데이터 0
                        PatrolName = item.PATROL_NAME, // 순찰 상태 명 ex) 샘플데이터 "방문객"
                        CarNum = item.CAR_NUM ?? string.Empty, // 차량 번호 ex) 샘플데이터 "99버9999"
                        PatrolImg = item.PATROL_IMG, // 순찰 이미지 ex) 샘플데이터 "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg"
                        PatrolRemark = item.PATROL_REMARK, // 순찰 비고 ex) 샘플데이터 ""
                        CreateDt = NowDate
                    });
                }

                int result = await TheHamBizRepository.AddPatrolAsync(PatrolTB, PatrolList);
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
        public async Task<ResponseUnit<PageNationDTO<InOutViewListDTO>?>> InOutViewListService(int pageNumber, int PageSize)
        {
            try
            {
                if (pageNumber == 0)
                    return new ResponseUnit<PageNationDTO<InOutViewListDTO>?>() { message = "필수값이 누락되었습니다.", data = null, code = 200 };
                if (PageSize == 0)
                    return new ResponseUnit<PageNationDTO<InOutViewListDTO>?>() { message = "필수값이 누락되었습니다.", data = null, code = 200 };

                var model = await TheHamBizRepository.InOutViewListAsync(pageNumber, PageSize);
                return new ResponseUnit<PageNationDTO<InOutViewListDTO>?>()
                {
                    message = "요청이 정상 처리되었습니다",
                    data = model,
                    code = 200
                };
            }catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return new ResponseUnit<PageNationDTO<InOutViewListDTO>?>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = null, code = 500 };
            }
        }

    }
}
