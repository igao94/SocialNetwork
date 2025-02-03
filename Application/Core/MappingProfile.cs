using Application.Helpers;
using Application.Photos.DTOs;
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
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos
                .FirstOrDefault(p => p.IsMain)!.Url));

        CreateMap<UpdateUserCommand, AppUser>();

        CreateMap<Photo, PhotoDto>();
    }
}
