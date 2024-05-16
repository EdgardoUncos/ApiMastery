﻿using System.ComponentModel.DataAnnotations;

namespace ApiMastery.DTOs
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
