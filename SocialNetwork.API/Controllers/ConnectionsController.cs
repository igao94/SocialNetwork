using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Repository;
using System.Security.Claims;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IMapper _mapper;

        public ConnectionsController(ISocialNetworkRepository socialNetworkRepository, IMapper mapper)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("{secondUserId}")]
        public async Task<ActionResult> CreateConnection(int secondUserId)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            var firstUserId = userId;

            if (firstUserId == secondUserId) return BadRequest("You can't connect with yourself.");

            var secondUser = await _socialNetworkRepository.GetUserByIdAsync(secondUserId);

            if (secondUser == null) return NotFound();

            var existingConnection = await _socialNetworkRepository
                .GetConnectionAsync(firstUserId, secondUserId);

            if (existingConnection != null) return BadRequest("You are already connected.");

            await _socialNetworkRepository.AddConnectionAsync(firstUserId, secondUserId);

            return Ok("Connection created successfully.");
        }

        [HttpDelete("{secondUserId}")]
        public async Task<ActionResult> DeleteConnection(int secondUserId)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId); 
            }

            var firstUserId = userId;

            var connection = await _socialNetworkRepository.GetConnectionAsync(firstUserId, secondUserId);

            if (connection == null) return NotFound();

            await _socialNetworkRepository.DeleteConnectionAsync(connection);

            return NoContent();
        }
    }
}
