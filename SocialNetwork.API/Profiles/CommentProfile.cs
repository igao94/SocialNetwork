using AutoMapper;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CommentForCreationDto>().ReverseMap();
            CreateMap<Comment, CommentForUpdateDto>().ReverseMap();
        }
    }
}
