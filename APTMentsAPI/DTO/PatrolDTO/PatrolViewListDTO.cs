namespace APTMentsAPI.DTO.PatrolDTO
{
    public class PatrolViewListDTO
    {
        /// <summary>
        /// PID (필수)
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 주차장ID (필수)
        /// </summary>
        public string? parkId { get; set; }

        /// <summary>
        /// 순찰 담당자 이름 (필수)
        /// </summary>
        public string? patrolUserNm { get; set; }

        /// <summary>
        /// 순찰 일시 (필수)
        /// </summary>
        public DateTime patrolDtm { get; set; }

        /// <summary>
        /// 순찰 상태 코드 (필수)
        /// 0: 정상(입주민), 1: 방문객, 2: 순찰, 3: 위반(블랙리스트)
        /// </summary>
        public int patrolCode { get; set; }

        /// <summary>
        /// 순찰 상태명 (필수아님)
        /// </summary>
        public string? patrolName { get; set; }

        /// <summary>
        /// 차량 번호 (필수)
        /// </summary>
        public string? carNum { get; set; }

        /// <summary>
        /// 순찰 이미지 (필수아님)
        /// </summary>
        public string? patrolImg { get; set; }

        /// <summary>
        /// 순찰 비고 (필수아님)
        /// </summary>
        public string? patrolRemark { get; set; }
    }

}
