using AutoMapper;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeDto>().ReverseMap();
            CreateMap<Like, LikeForCreationDto>().ReverseMap();
        }
    }
}
