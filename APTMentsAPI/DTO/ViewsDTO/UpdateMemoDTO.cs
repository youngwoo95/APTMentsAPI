using System.ComponentModel.DataAnnotations;

namespace APTMentsAPI.DTO.ViewsDTO
{
    public class UpdateMemoDTO
    {
        /// <summary>
        /// 시스템 시퀀스 번호
        /// </summary>
        [Required]
        public int pId { get; set; }

        /// <summary>
        /// 수정할 메모 내용
        /// </summary>
        public string? memo { get; set; }
    }
}
