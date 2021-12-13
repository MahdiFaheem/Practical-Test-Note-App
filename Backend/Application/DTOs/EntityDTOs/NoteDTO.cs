using Application.Validators;
using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.EntityDTOs
{
    public class NoteDTO
    {
        [Required, MaxLength(100)]
        public string NoteMessage { get; set; }
        [Required]
        public NoteTypeEnum NoteType { get; set; }
        [NoteDateValidator]
        public DateTime? NoteDate { get; set; }
        [NoteTodoCompleteValidator]
        public bool? IsComplete { get; set; }
        public string UserId { get; set; }
    }
}
