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
    public class LikesController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IMapper _mapper;

        public LikesController(ISocialNetworkRepository socialNetworkRepository, IMapper mapper)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("AddLike")]
        public async Task<ActionResult> AddLike(LikeForCreationDto likeForCreationDto)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            likeForCreationDto.UserId = userId;

            var post = await _socialNetworkRepository.GetPostByIdAsync(likeForCreationDto.PostId);

            if (post == null) return NotFound();

            var like = _mapper.Map<Like>(likeForCreationDto);

            await _socialNetworkRepository.AddLikeAsync(like);

            return Ok("Like added successfully.");
        }

        [HttpDelete("{likeId}")]
        public async Task<ActionResult> DeleteLike(int likeId)
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

            var like = await _socialNetworkRepository.GetLikeByIdAsync(likeId);

            if (like == null) return NotFound();

            if (like.UserId != userId && !isAdmin) return Forbid();

            await _socialNetworkRepository.DeleteLikeAsync(like);

            return NoContent();
        }
    }
}
