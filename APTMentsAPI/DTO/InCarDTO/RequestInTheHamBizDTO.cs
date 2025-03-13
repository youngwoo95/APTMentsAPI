namespace APTMentsAPI.DTO.InCarDTO
{
    /// <summary>
    /// 입차 요청 - 더함비즈 API
    /// </summary>
    public class RequestInTheHamBizDTO
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
        /// 입출 상태명
        /// </summary>
        public string? IO_STATUS_TP_NM { get; set; }

        /// <summary>
        /// 입차 GATE ID
        /// </summary>
        public string? IN_GATE_ID { get; set; }

        /// <summary>
        /// 입차 GATE NM
        /// </summary>
        public string? IN_GATE_NM { get; set; }

        /// <summary>
        /// 입차 라인 번호
        /// </summary>
        public int IN_LINE_NUM { get; set; }

        /// <summary>
        /// 입차 일시 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string? IN_DTM { get; set; }

        /// <summary>
        /// 입차 LPR 상태
        /// </summary>
        public string? IN_LPR_STATUS { get; set; }

        /// <summary>
        /// 입차 LPR 상태 명칭
        /// </summary>
        public string? IN_LPR_STATUS_NM { get; set; }

        /// <summary>
        /// /입차 구분 # 2 : 일반차량, 6 : 정기차량
        /// </summary>
        public string? IN_TICKET_TP { get; set; }

        /// <summary>
        /// 입차 차량 구분 명
        /// </summary>
        public string? IN_TICKET_TP_NM { get; set; }

        /// <summary>
        /// 블랙리스트 정보
        /// </summary>
        public RequestInBlackListInfoDTO BLACK_LIST_INFO { get; set; }

        /// <summary>
        /// 이미지 경로 (HTTP 웹 경로)
        /// </summary>
        public string? IMG_PATH { get; set; }

        /// <summary>
        /// 동 (정기권, 예약차량일 경우 동 정보)
        /// </summary>
        public string? DONG { get; set; }

        /// <summary>
        /// 호 (정기권, 예약차량일 경우 호 정보)
        /// </summary>
        public string? HO { get; set; }

        /// <summary>
        /// 예약 차량 여부 (0, 1)
        /// </summary>
        public string? IS_RESERVATION { get; set; }

        /// <summary>
        /// 입차 처리/대기 여부 (0, 1, 2)
        /// 0: 입차, 1: 입차대기, 2: 입차대기 후 승인
        /// </summary>
        public string? IS_WAIT { get; set; }

        /// <summary>
        /// 대기 걸린 차량의 이유
        /// </summary>
        public string? IS_WAIT_REASON { get; set; }

        /// <summary>
        /// 기타 정보 (예약 사유, 방문차량 사유 등)
        /// </summary>
        public string? ETC { get; set; }

    }


    public class RequestInBlackListInfoDTO
    {
        /// <summary>
        /// 블랙리스트 여부 (0 또는 1)
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
