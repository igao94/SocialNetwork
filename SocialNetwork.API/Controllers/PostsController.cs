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
    public class PostsController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IMapper _mapper;

        public PostsController(ISocialNetworkRepository socialNetworkRepository, IMapper mapper)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{userId}/GetPostsForUser")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts(int userId)
        {
            if (!await _socialNetworkRepository.UserExistsAsync(userId)) return NotFound();

            var posts = await _socialNetworkRepository.GetPostsAsync(userId);

            return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpGet("{userId}/{postId}", Name = "GetPost")]
        public async Task<ActionResult<PostDto>> GetPost(int userId, int postId)
        {
            if (!await _socialNetworkRepository.UserExistsAsync(userId)) return NotFound();

            var post = await _socialNetworkRepository.GetPostAsync(userId, postId);

            if (post == null) return NotFound();

            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpPost]
        public async Task<ActionResult<PostWithoutCommentsAndLikesDto>> CreatePost(PostForCreationDto postForCreationDto)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            postForCreationDto.UserId = userId;

            var post = _mapper.Map<Post>(postForCreationDto);

            await _socialNetworkRepository.AddPostAsync(post);

            var postDto = _mapper.Map<PostWithoutCommentsAndLikesDto>(post);

            return CreatedAtRoute(nameof(GetPost), new { userId, postDto.PostId }, postDto);
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult> DeletePost(int postId)
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

            var post = await _socialNetworkRepository.GetPostByIdAsync(postId);

            if (post == null) return NotFound();

            if (post.UserId != userId && !isAdmin) return Forbid();

            await _socialNetworkRepository.DeletePostAsync(post);

            return NoContent();
        }
    }
}
