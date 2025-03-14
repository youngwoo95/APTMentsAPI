using APTMentsAPI.DBModels;
using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;
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

                var ParkingHistoryTB = new IoParkingHistory
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
                    IsWait = dto.IS_WAIT, // 입차 처리/대기 여부 (0, 1, 2) [NOT NULL]
                    IsWaitReason = dto.IS_WAIT_REASON, // 대기 걸린 차량의 이유
                    ImgPath = dto.IMG_PATH, // 이미지경로 [NOT NULL]
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
               
            
                int result = await TheHamBizRepository.AddInCar(ParkingHistoryTB);
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

                var ParkingHistoryTB = new IoParkingHistory();

                ParkingHistoryTB.IoGubun = 1; // IO 구분 * 0: 출차 1: 입차 [NOT NULL]
                ParkingHistoryTB.IoSeq = dto.IO_SEQ; // 입출차 일련번호 [NOT NULL]
                ParkingHistoryTB.ParkId = dto.PARK_ID; // 주차장 ID [NOT NULL]
                ParkingHistoryTB.CarNum = dto.CAR_NUM; // 차량 번호 [NOT NULL]
                ParkingHistoryTB.IoStatusTp = dto.IO_STATUS_TP; // 입출 상태 [NOT NULL]
                ParkingHistoryTB.IoStatusTpNm = dto.IO_STATUS_TP_NM; // 입출 상태명 [NOT NULL]
                ParkingHistoryTB.IoGateId = dto.OUT_GATE_ID; // 입차 GATE ID [NOT NULL]
                ParkingHistoryTB.IoGateNm = dto.OUT_GATE_NM; // 입차 GATE NM [NOT NULL]
                ParkingHistoryTB.IoLineNum = dto.OUT_LINE_NUM; // 입차 라인 번호 [NOT NULL]
                ParkingHistoryTB.IoDtm = DateTime.Parse(dto.OUT_DTM); // 입차 일시 [NOT NULL]
                ParkingHistoryTB.IoLprStatus = dto.OUT_LPR_STATUS; // 입차 LPR 상태 ID
                ParkingHistoryTB.IoLprStatusNm = dto.OUT_LPR_STATUS_NM; // 입차 LPR 상태 명칭
                ParkingHistoryTB.IoTicketTp = dto.OUT_TICKET_TP; // 입차 차량 구분 ID [NOT NULL]
                ParkingHistoryTB.IoTicketTpNm = dto.OUT_TICKET_TP_NM; // 입차 차량 구분 [NOT NULL]
                ParkingHistoryTB.Dong = dto.DONG; // 동
                ParkingHistoryTB.Ho = dto.HO; // 호
                ParkingHistoryTB.IsReservation = dto.IS_RESERVATION; // 예약차량여부

                if (dto.BLACK_LIST_INFO is not null)
                {
                    ParkingHistoryTB.IsBlackList = dto.BLACK_LIST_INFO.IS_BLACK_LIST; // 블랙리스트 여부
                    ParkingHistoryTB.BlackListReason = dto.BLACK_LIST_INFO.BLACK_LIST_REASON; // 블랙리스트 사유
                    ParkingHistoryTB.RegDtm = dto.BLACK_LIST_INFO.REG_DTM; // 블랙리스트 등록일자 처리 // 블랙리스트 등록일자
                }
                else
                {
                    ParkingHistoryTB.IsBlackList = "0";
                    ParkingHistoryTB.BlackListReason = null;
                    ParkingHistoryTB.RegDtm = null;
                }

                ParkingHistoryTB.ImgPath = dto.IMG_PATH; // 이미지경로 [NOT NULL]
                ParkingHistoryTB.Etc = dto.ETC; // 기타 정보
                ParkingHistoryTB.CreateDt = DateTime.Now;
                ParkingHistoryTB.ParkDuration = dto.PARK_DURATION; // 주차 시간
                ParkingHistoryTB.VisitTime = dto.VISIT_TIME; // 방문 시간

                int result = await TheHamBizRepository.AddOutCar(ParkingHistoryTB);

                return 1;
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
                return -1;
            }
        }
    }
}
