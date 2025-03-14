namespace APTMentsAPI.DTO.OutCarDTO
{
    public class RequestOutTheHamBizDTO
    {
        /// <summary>
        /// 입출차 일련번호
        /// </summary>
        public string? IO_SEQ { get; set; }

        /// <summary>
        /// 주차장 ID
        /// </summary>
        public string? PARK_ID { get; set; }

        /// <summary>
        /// 차량 번호
        /// </summary>
        public string? CAR_NUM { get; set; }

        /// <summary>
        /// 입출 상태
        /// </summary>
        public string? IO_STATUS_TP { get; set; }

        /// <summary>
        /// 입출 상태 명
        /// </summary>
        public string? IO_STATUS_TP_NM { get; set; }

        /// <summary>
        /// 출차 GATE ID
        /// </summary>
        public string? OUT_GATE_ID { get; set; }

        /// <summary>
        /// 출차 GATE NM
        /// </summary>
        public string? OUT_GATE_NM { get; set; }

        /// <summary>
        /// 출차 라인번호
        /// </summary>
        public int OUT_LINE_NUM { get; set; }

        /// <summary>
        /// 출차 일시 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string? OUT_DTM { get; set; }

        /// <summary>
        /// 출차 LPR 상태
        /// </summary>
        public string? OUT_LPR_STATUS { get; set; }

        /// <summary>
        /// 출차 LPR 상태 명칭
        /// </summary>
        public string? OUT_LPR_STATUS_NM { get; set; }

        /// <summary>
        /// 출차 차량 구분
        /// </summary>
        public string? OUT_TICKET_TP { get; set; }

        /// <summary>
        /// 출차 차량 구분 명
        /// </summary>
        public string? OUT_TICKET_TP_NM { get; set; }

        /// <summary>
        /// 동
        /// </summary>
        public string? DONG { get; set; }

        /// <summary>
        /// 호
        /// </summary>
        public string? HO { get; set; }

        /// <summary>
        /// 예약차량여부 # 0: 일반, 1: 일반 2: 홈넷 예약
        /// </summary>
        public string? IS_RESERVATION { get; set; }

        /// <summary>
        /// 블랙리스트 정보
        /// </summary>
        public RequestOutBlackListInfoDTO? BLACK_LIST_INFO { get; set; }

        /// <summary>
        /// 이미지 경로 #http 웨ㅐㅂ 경로로 받아야함.
        /// </summary>
        public string? IMG_PATH { get; set; }

        /// <summary>
        /// 예약차량일 경우 (예약 사유, 방문차량일경우 방문사유)
        /// </summary>
        public string? ETC { get; set; }

        /// <summary>
        /// 주차 시간
        /// </summary>
        public int PARK_DURATION { get; set; }

        /// <summary>
        /// 방문 시간 (방문포인트 사용 시간)
        /// </summary>
        public int VISIT_TIME { get; set; }
    }

    public class RequestOutBlackListInfoDTO
    {
        /// <summary>
        /// 블랙리스트 여부 (입차처리 * 블랙리스트로 표시) OR 입차대기 걸고 블랙리스트 표시
        /// </summary>
        public string? IS_BLACK_LIST { get; set; }

        /// <summary>
        /// 블랙리스트 사유
        /// </summary>
        public string? BLACK_LIST_REASON { get; set; }

        /// <summary>
        /// 등록 일시
        /// </summary>
        public string? REG_DTM { get; set; }

    }
}
