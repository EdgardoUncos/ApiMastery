using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ApiMastery.DTOs
{
    public class TokenRequest
    {
        [Required]
        public string Token {  get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
