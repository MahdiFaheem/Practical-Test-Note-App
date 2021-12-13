using Application.DTOs.EntityDTOs;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Validators
{
    public class NoteDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var noteDTO = validationContext.ObjectInstance as NoteDTO;
            if (noteDTO.NoteType == NoteTypeEnum.Reminder || noteDTO.NoteType == NoteTypeEnum.Todo)
            {
                return value == null ? new ValidationResult("Date is required.") : null;
            }

            return null;
        }
    }
}
