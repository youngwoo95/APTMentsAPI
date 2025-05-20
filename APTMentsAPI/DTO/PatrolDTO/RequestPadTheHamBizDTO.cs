using System.ComponentModel.DataAnnotations;

namespace APTMentsAPI.DTO.PatrolDTO
{
    public class RequestPadTheHamBizDTO
    {
        /// <summary>
        /// 주차장 ID
        /// </summary>
        public string? PARK_ID { get; set; }

        /// <summary>
        /// 순찰 담당자 ID
        /// </summary>
        [Required]
        public string PATROL_USER_ID { get; set; } = null!;

        /// <summary>
        /// 순찰 담당자 이름
        /// </summary>
        public string? PATROL_USER_NM { get; set; }

        /// <summary>
        /// 순찰 일시 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string? PATROL_START_DTM { get; set; }

        /// <summary>
        /// 순찰 종료 일시 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string? PATROL_END_DTM { get; set; }

        /// <summary>
        /// 전체 데이터 개수
        /// </summary>
        public int TOT_CNT { get; set; }

        public List<PatListInfoDTO>? PAY_LOAD { get; set; } = new List<PatListInfoDTO>();


    }

    public class PatListInfoDTO
    {
        /// <summary>
        /// 순찰 일시 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string? PATROL_DTM { get; set; }

        /// <summary>
        /// 차량 구분 2. 방문차량 6. 정기차량
        /// </summary>
        public string IO_TICKET_TP { get; set; } = null!;

        /// <summary>
        /// 순찰 코드 1. 순찰, 2. 위반(블랙리스트)
        /// </summary>
        public int PATROL_CD { get; set; }

        /// <summary>
        /// 순찰 상태 명
        /// </summary>
        public string? PATROL_NAME { get; set; }

        /// <summary>
        /// 차량 번호
        /// </summary>
        public string? CAR_NUM { get; set; }

        /// <summary>
        /// 순찰 이미지
        /// </summary>
        public string? PATROL_IMG { get; set; }

        /// <summary>
        /// 순찰 비고
        /// </summary>
        public string? PATROL_REMARK { get; set; }

    }
}
