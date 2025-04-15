using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APTMentsAPI.DTO.IpDTO
{
    public class IpSettingDTO
    {
        /// <summary>
        /// IP 설정 값
        /// </summary>
        [Required]
        public string? IpAddress { get; set; }
    }
}
