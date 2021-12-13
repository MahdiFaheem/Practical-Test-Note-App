using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthenticationDTOs
{
    public class RegisterDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
