namespace APTMentsAPI.DTO.ViewsDTO
{
    public class DetailViewDTO
    {
        /// <summary>
        /// 시스템 PID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 입출차 구분
        /// </summary>
        public int ioGubun { get; set; }

        /// <summary>
        /// 시퀀스
        /// </summary>
        public string? ioSeq { get; set; }

        /// <summary>
        /// 주차장 ID
        /// </summary>
        public string? parkId { get; set; }

        /// <summary>
        /// 차량 번호
        /// </summary>
        public string? carNum { get; set; }

        /// <summary>
        /// 입출 상태
        /// </summary>
        public string? ioStatusTp { get; set; }

        /// <summary>
        /// 입출 상태 명
        /// </summary>
        public string? ioStatusTpNm { get; set; }

        /// <summary>
        /// 입-출차 GATE ID
        /// </summary>
        public string? ioGateId { get; set; }

        /// <summary>
        /// 입-출차 GATE NM
        /// </summary>
        public string? ioGateNm { get; set; }

        /// <summary>
        /// 입-출차 라인 번호
        /// </summary>
        public int ioLineNum { get; set; }

        /// <summary>
        /// 입-출차 일시
        /// </summary>
        public DateTime? ioDtm { get; set; }

        /// <summary>
        /// 입-출차 LPR 상태 ID
        /// </summary>
        public string? ioLprStatus { get; set; }

        /// <summary>
        /// 입-출차 LPR 상태 명
        /// </summary>
        public string? ioLprStatusNm { get; set; }

        /// <summary>
        /// 입-출차 차량 구분 ID
        /// </summary>
        public string? ioTicketTp { get; set; }

        /// <summary>
        /// 입-출차 차량 구분
        /// </summary>
        public string? ioTicketTpNm { get; set; }

        /// <summary>
        /// 동
        /// </summary>
        public string? dong { get; set; }

        /// <summary>
        /// 호
        /// </summary>
        public string? ho { get; set; }

        /// <summary>
        /// 예약차량 여부
        /// </summary>
        public string? isReservation { get; set; }

        /// <summary>
        /// 블랙리스트 여부
        /// </summary>
        public string? isBlacklist { get; set; }

        /// <summary>
        /// 블랙리스트 사유
        /// </summary>
        public string? blacklistReason { get; set; }

        /// <summary>
        /// 블랙리스트 등록 일시
        /// </summary>
        public string? regDtm { get; set; }

        /// <summary>
        /// 이미지 경로
        /// </summary>
        public byte[]? imgPath { get; set; }

        /// <summary>
        /// 해당차량을 입차처리할건지 대기할건지
        /// </summary>
        public string? isWait { get; set; }

        /// <summary>
        /// 대기걸린 차량의 이유
        /// </summary>
        public string? isWaitReason { get; set; }

        /// <summary>
        /// 주차시간
        /// </summary>
        public int? parkDuration { get; set; }

        /// <summary>
        /// 방문포인트 사용시간
        /// </summary>
        public int? visitTime { get; set; }

        /// <summary>
        /// 예약 차량의 경우
        /// </summary>
        public string? etc { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        public string? memo { get; set; }

    }
}
