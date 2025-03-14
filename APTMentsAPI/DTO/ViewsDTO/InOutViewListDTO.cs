namespace APTMentsAPI.DTO.ViewsDTO
{
    public class InOutViewListDTO
    {
        /// <summary>
        /// 시퀀스
        /// </summary>
        public string? IO_SEQ { get; set; }

        /// <summary>
        /// 입출유형 구분
        /// 방문차량, 예약차량 - 2
        /// 정기차량 - 6
        /// </summary>
        public string? IO_TICKET_TP { get; set; }

        /// <summary>
        /// 입출유형 구분 명칭
        /// </summary>
        public string? IO_TICKET_TP_NM { get; set; }

        /// <summary>
        /// 입-출차 구분
        /// </summary>
        public string? IO_STATUS_TP { get; set; }

        /// <summary>
        /// 입-출차 구분 명칭
        /// </summary>
        public string? IO_STATUS_TP_NM { get; set; }

        /// <summary>
        /// 차량 번호
        /// </summary>
        public string? CAR_NUM { get; set; }

        /// <summary>
        /// 입차 시간
        /// </summary>
        public DateTime? IN_DTM { get; set; }

        /// <summary>
        /// 출차 시간
        /// </summary>
        public DateTime? OUT_DTM { get; set; }

        /// <summary>
        /// 주차 시간
        /// </summary>
        public int ParkingDuration { get; set; }

        /// <summary>
        /// 입차 게이트 구분
        /// </summary>
        public string? IN_GATE_ID { get; set; }

        /// <summary>
        /// 입차 게이트 구분 명칭
        /// </summary>
        public string? IN_GATE_NM { get; set; }

        /// <summary>
        /// 출차 게이트 구분
        /// </summary>
        public string? OUT_GATE_ID { get; set; }

        /// <summary>
        /// 출차 게이트 구분 명칭
        /// </summary>
        public string? OUT_GATE_NM { get; set; }

        /// <summary>
        /// 동
        /// </summary>
        public string? DONG { get; set; }

        /// <summary>
        /// 호
        /// </summary>
        public string? HO { get; set; }

        /// <summary>
        /// 입차 이미지
        /// </summary>
        public string? IN_IMG_PATH { get; set; }

        /// <summary>
        /// 출차 이미지
        /// </summary>
        public string? OUT_IMG_PATH { get; set; }
    }
}
