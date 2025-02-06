using Application.Comments.DTOs;
using Application.Helpers;
using Application.Likes.DTOs;
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
                .FirstOrDefault(p => p.IsMain)!.Url))
            .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest => dest.FollowingCount, opt => opt.MapFrom(src => src.Following.Count));

        CreateMap<UpdateUserCommand, AppUser>();

        CreateMap<Photo, PhotoDto>();

        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.AppUser.Photos
                .FirstOrDefault(p => p.IsMain)!.Url))
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count));

        CreateMap<PostPhoto, PostPhotoDto>();

        CreateMap<UpdatePostCommand, Post>();

        CreateMap<AppUser, UserLikeDto>()
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.Photos
                .FirstOrDefault(p => p.IsMain)!.Url));

        CreateMap<AppUserPostComment, CommentDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.AppUser.Photos
                .FirstOrDefault(p => p.IsMain)!.Url));
    }
}
