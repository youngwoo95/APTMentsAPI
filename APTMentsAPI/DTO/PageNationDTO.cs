namespace APTMentsAPI.DTO
{
    public class PageNationDTO<T>
    {
        /// <summary>
        /// 현재 페이지의 데이터 리스트
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// 요청한 페이지 번호
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 한 페이지당 항목 수
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 전체 데이터 수 
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// 전체 페이지 수
        /// </summary>
        public int totalPages => (int)Math.Ceiling((double)totalCount / pageSize);

    }
}
