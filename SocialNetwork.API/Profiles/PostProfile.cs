using AutoMapper;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostWithoutCommentsAndLikesDto>().ReverseMap();
            CreateMap<Post, PostForCreationDto>().ReverseMap();
        }
    }
}
