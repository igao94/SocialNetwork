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
    public class CommentsController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IMapper _mapper;

        public CommentsController(ISocialNetworkRepository socialNetworkRepository, IMapper mapper)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("AddComment")]
        public async Task<ActionResult> AddComment(CommentForCreationDto commentForCreationDto)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            commentForCreationDto.UserId = userId;

            var post = await _socialNetworkRepository.GetPostByIdAsync(commentForCreationDto.PostId);

            if (post == null) return NotFound();

            var comment = _mapper.Map<Comment>(commentForCreationDto);

            await _socialNetworkRepository.AddCommentAsync(comment);

            return Ok("Comment added successfully.");
        }

        [HttpPut("{commentId}/EditComment")]
        public async Task<ActionResult> EditComment(int commentId, CommentForUpdateDto commentForUpdateDto)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            commentForUpdateDto.UserId = userId;

            var comment = await _socialNetworkRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound();

            _mapper.Map(commentForUpdateDto, comment);

            await _socialNetworkRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId)
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

            var comment = await _socialNetworkRepository.GetCommentByIdAsync(commentId);

            if (comment == null) return NotFound();

            if (comment.UserId != userId && !isAdmin) return Forbid();

            await _socialNetworkRepository.DeleteCommentAsync(comment);

            return NoContent();
        }
    }
}
