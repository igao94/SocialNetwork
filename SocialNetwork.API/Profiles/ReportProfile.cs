using AutoMapper;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportUser, ReportUserForCreationDto>().ReverseMap();
            CreateMap<ReportUser, ReportUserDto>().ReverseMap();

            CreateMap<ReportPost, ReportPostForCreationDto>().ReverseMap();
            CreateMap<ReportPost, ReportPostDto>().ReverseMap();
        }
    }
}
