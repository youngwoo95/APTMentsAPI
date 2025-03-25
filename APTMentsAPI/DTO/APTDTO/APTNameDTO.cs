using System.ComponentModel.DataAnnotations;

namespace APTMentsAPI.DTO.APTDTO
{
    public class APTNameDTO
    {
        /// <summary>
        /// 아파트 명칭
        /// </summary>
        [Required]
        public string? APTName { get; set; }
    }
}
