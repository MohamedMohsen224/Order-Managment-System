﻿using System.ComponentModel.DataAnnotations;

namespace Order_Managment_System.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
