namespace SocialNetwork.API.Models.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public int NumberOfConnections
        {
            get
            {
                return Connections.Count();
            }
        }
        public IEnumerable<ConnectionDto> Connections { get; set; } =
            new List<ConnectionDto>();
        public int NumberOfPosts
        {
            get
            {
                return Posts.Count();
            }
        }
        public IEnumerable<PostDto> Posts { get; set; } =
            new List<PostDto>();
    }
}
