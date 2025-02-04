using Application.Helpers;
using Application.Photos.DTOs;
using Application.Posts.DTOs;
using Application.Posts.UpdatePost;
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

        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.AppUser.Photos
                .FirstOrDefault(p => p.IsMain)!.Url));

        CreateMap<PostPhoto, PostPhotoDto>();

        CreateMap<UpdatePostCommand, Post>();
    }
}
