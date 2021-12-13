using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Note
    {
        public string Id { get; set; }
        [Required]
        public NoteTypeEnum NoteType { get; set; }
        [Required, MaxLength(100)]
        public string NoteMessage { get; set; }
        public DateTime? NoteDate { get; set; }
        public bool? IsComplete { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
