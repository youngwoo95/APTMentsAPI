namespace APTMentsAPI.DTO.ViewsDTO
{
    public class InOutViewListDTO
    {
        /// <summary>
        /// 시퀀스
        /// </summary>
        public string? ioSeq { get; set; }

        /// <summary>
        /// 입출유형 구분
        /// 방문차량, 예약차량 - 2
        /// 정기차량 - 6
        /// </summary>
        public string? ioTicketTp { get; set; }

        /// <summary>
        /// 입출유형 구분 명칭
        /// </summary>
        public string? ioTicketTpNm { get; set; }

        /// <summary>
        /// 입-출차 구분
        /// </summary>
        public string? ioStatusTp { get; set; }

        /// <summary>
        /// 입-출차 구분 명칭
        /// </summary>
        public string? ioStatusTpNm { get; set; }

        /// <summary>
        /// 차량 번호
        /// </summary>
        public string? carNum { get; set; }

        /// <summary>
        /// 입차 시간
        /// </summary>
        public DateTime? inDtm { get; set; }

        /// <summary>
        /// 출차 시간
        /// </summary>
        public DateTime? outDtm { get; set; }

        /// <summary>
        /// 주차 시간
        /// </summary>
        public int parkingDuration { get; set; }

        /// <summary>
        /// 입차 게이트 구분
        /// </summary>
        public string? inGateId { get; set; }

        /// <summary>
        /// 입차 게이트 구분 명칭
        /// </summary>
        public string? inGateNm { get; set; }

        /// <summary>
        /// 출차 게이트 구분
        /// </summary>
        public string? outGateId { get; set; }

        /// <summary>
        /// 출차 게이트 구분 명칭
        /// </summary>
        public string? outGateNm { get; set; }

        /// <summary>
        /// 동
        /// </summary>
        public string? dong { get; set; }

        /// <summary>
        /// 호
        /// </summary>
        public string? ho { get; set; }

        /// <summary>
        /// 입차 이미지
        /// </summary>
        public string? inImagePath { get; set; }

        /// <summary>
        /// 출차 이미지
        /// </summary>
        public string? outImagePath { get; set; }

        /// <summary>
        /// 블랙리스트 여부
        /// </summary>
        public string? isBlacklist { get; set; }

        /// <summary>
        /// 블랙리스트 사유
        /// </summary>
        public string? blacklistReason { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        public string? memo { get; set; }
    }
}
