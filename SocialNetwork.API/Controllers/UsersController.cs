using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;
using SocialNetwork.API.Repository;
using System.Security.Claims;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IMapper _mapper;
        const int MaxPageSize = 20;

        public UsersController(ISocialNetworkRepository socialNetworkRepository, IMapper mapper)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWithoutPostsAndConnectionsDto>>> GetUsers(string? searchQuery,
            int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > MaxPageSize) pageSize = MaxPageSize;

            var users = await _socialNetworkRepository.GetUsersAsync(searchQuery, pageNumber, pageSize);

            return Ok(_mapper.Map<IEnumerable<UserWithoutPostsAndConnectionsDto>>(users));
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _socialNetworkRepository.GetUserAsync(id);

            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserWithoutPostsAndConnectionsDto>> CreateUser(UserForCreationDto userForCreationDto)
        {
            if (await _socialNetworkRepository.UserEmailExistsAsync(userForCreationDto.Email))
            {
                return BadRequest("User with that email already exists.");
            }

            var user = _mapper.Map<User>(userForCreationDto);

            await _socialNetworkRepository.AddUserAsync(user);

            var userDto = _mapper.Map<UserWithoutPostsAndConnectionsDto>(user);

            return CreatedAtRoute(nameof(GetUser), new { id = userDto.UserId }, userDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            bool isAdmin = false;

            if (currentUser.Claims.Any())
            {
                isAdmin = currentUser.Claims.ToList().Where(t => t.Type == "admin").FirstOrDefault()!.Value
                    == "True";
            }

            var user = await _socialNetworkRepository.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            if (user.UserId != userId && !isAdmin) return Forbid();

            await _socialNetworkRepository.DeleteUserAsync(user);

            return NoContent();
        }
    }
}
