﻿using System.ComponentModel.DataAnnotations;

namespace SwapApp.Models
{
    public class RegisterUserDto
    {
        [Required]
         public string Email { get; set; }
        [Required]
         public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        public int RoleId { get; set; } = 1;

    }
}
