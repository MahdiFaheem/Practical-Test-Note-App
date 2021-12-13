using Application.DTOs.EntityDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingConfigurations
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<NoteDTO, Note>().ReverseMap();
        }
    }
}
