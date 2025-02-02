using Application.Helpers;
using Application.Users.DTOs;
using Application.Users.UpdateUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

        CreateMap<UpdateUserCommand, AppUser>();
    }
}
