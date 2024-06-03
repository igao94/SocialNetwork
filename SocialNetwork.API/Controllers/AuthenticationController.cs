using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Repository;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ISocialNetworkRepository socialNetworkRepository,
            IConfiguration configuration)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = await ValidateUserCredentialsAsync(authenticationRequestBody.Email,
                authenticationRequestBody.Password);

            if (user == null) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new("sub", user.UserId.ToString()),
                new("given_name", user.FirstName),
                new("family_name", user.LastName),
                new("admin", user.IsAdmin.ToString()),
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        [HttpPost("SendResetPasswordLink")]
        public async Task<ActionResult<string>> CreateToken(string email)
        {
            var user = await _socialNetworkRepository.GetUserByEmailAsync(email);

            if (user == null) return NotFound();

            var token = await _socialNetworkRepository.CreateTokenAsync(user.Email);

            return Ok(token);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> Reset(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var userId = await _socialNetworkRepository.GetUserIdByTokenAsync(resetPasswordRequestDto.Token);

            if (userId == 0) return NotFound();

            await _socialNetworkRepository.SetPasswordAsync(userId, resetPasswordRequestDto.Password);

            return Ok();
        }

        public class AuthenticationRequestBody
        {
            [Required, EmailAddress]
            public string? Email { get; set; }
            [Required, MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
            public string? Password { get; set; }
        }

        private class SocialNetworkUser
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool IsAdmin { get; set; }

            public SocialNetworkUser(int userId, string email, string firstName, string lastName, bool isAdmin)
            {
                UserId = userId;
                Email = email;
                FirstName = firstName;
                LastName = lastName;
                IsAdmin = isAdmin;
            }
        }

        private async Task<SocialNetworkUser?> ValidateUserCredentialsAsync(string? email, string? password)
        {
            var user = await _socialNetworkRepository.GetUserByCredentialsAsync(email, password);

            if (user != null)
            {
                return new SocialNetworkUser(
                    user.UserId,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.IsAdmin);
            }

            return null;
        }
    }
}
