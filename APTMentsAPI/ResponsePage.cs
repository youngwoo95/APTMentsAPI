namespace APTMentsAPI
{
    public class ResponsePage<T>
    {
        //public string? message { get; set; }
        public Meta? Metas { get; set; }
        public List<T>? data { get; set; }
        public int code { get; set; }
    }

    public class Meta
    {
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
