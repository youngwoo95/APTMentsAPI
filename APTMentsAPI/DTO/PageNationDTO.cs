namespace APTMentsAPI.DTO
{
    public class PageNationDTO<T>
    {
        /// <summary>
        /// 현재 페이지의 데이터 리스트
        /// </summary>
        public List<T>? Items { get; set; }
    }
}
