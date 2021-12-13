using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
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
