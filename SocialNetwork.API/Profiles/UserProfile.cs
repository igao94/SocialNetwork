using AutoMapper;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserWithoutPostsAndConnectionsDto>().ReverseMap();
            CreateMap<User, UserForCreationDto>().ReverseMap();
        }
    }
}
