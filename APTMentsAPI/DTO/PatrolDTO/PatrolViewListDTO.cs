namespace APTMentsAPI.DTO.PatrolDTO
{
    public class PatrolViewListDTO
    {
        /// <summary>
        /// PID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 주차장ID
        /// </summary>
        public string? parkId { get; set; }

        /// <summary>
        /// 순찰 담당자 ID
        /// </summary>
        public string? partolUserId { get; set; }

        /// <summary>
        /// 순찰 담당자 이름
        /// </summary>
        public int patrolUserNm { get; set; }

        /// <summary>
        /// 순찰 시작 일시
        /// </summary>
        public string? patrolStartDtm { get; set; }

        /// <summary>
        /// 순찰 종료 일시
        /// </summary>
        public string? patrolEndDtm { get; set; }

        /// <summary>
        /// 전체 데이터 개수
        /// </summary>
        public int totCnt { get; set; }

        /// <summary>
        /// Row LIST
        /// </summary>
        public List<PatrolLowList> lowList { get; set; } = new List<PatrolLowList>();
    }

    public class PatrolLowList
    {
        /// <summary>
        /// PID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 순찰 일시
        /// </summary>
        public DateTime patrolDtm { get; set; }

        /// <summary>
        /// 순찰 상태 코드
        /// 0: 정상(입주민), 1: 방문객, 2: 순찰, 3: 위반(블랙리스트)
        /// </summary>
        public int patrolCode { get; set; }

        /// <summary>
        /// 순찰 상태명
        /// </summary>
        public string? patrolName { get; set; }

        /// <summary>
        /// 차량 번호
        /// </summary>
        public string? carNum { get; set; }

        /// <summary>
        /// 순찰 이미지
        /// </summary>
        public string? patrolImg { get; set; }
        //public byte[]? patrolImg { get; set; }


        /// <summary>
        /// 순찰 비고
        /// </summary>
        public string? patrolRemark { get; set; }
    }
}
