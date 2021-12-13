using Application.DTOs.AuthenticationDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDTO, User>().ReverseMap();
        }
    }
}
