using AutoMapper;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Profiles
{
    public class ConnectionsProfile : Profile
    {
        public ConnectionsProfile()
        {
            CreateMap<Connection, ConnectionDto>().ReverseMap();
        }
    }
}
