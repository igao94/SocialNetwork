﻿namespace SocialNetwork.API.Models.DTO
{
    public class UserWithoutPostsAndConnectionsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
