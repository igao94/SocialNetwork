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
    public class ReportsController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IMapper _mapper;

        public ReportsController(ISocialNetworkRepository socialNetworkRepository, IMapper mapper)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("ReportUser")]
        public async Task<ActionResult> ReportUser(ReportUserForCreationDto reportUserForCreationDto)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            reportUserForCreationDto.UserId = userId;

            if (reportUserForCreationDto.UserId == reportUserForCreationDto.ReportedUserId)
                return BadRequest("You can't report yourself.");

            var user = await _socialNetworkRepository
                .GetUserByIdAsync(reportUserForCreationDto.ReportedUserId);

            if (user == null) return NotFound();

            var reportedUser = await _socialNetworkRepository
                .GetReportedUserAsync(reportUserForCreationDto.ReportedUserId);

            if (reportedUser != null) return BadRequest("You already reported that user.");

            var userToReport = _mapper.Map<ReportUser>(reportUserForCreationDto);

            await _socialNetworkRepository.AddUserReportAsync(userToReport);

            return Ok("User reported successfully.");
        }

        [HttpGet("GetReportedUsers")]
        public async Task<ActionResult<IEnumerable<ReportUserDto>>> GetReportedUsers()
        {
            ClaimsPrincipal currentUser = User;

            bool isAdmin = false;

            if (currentUser.Claims.Any())
            {
                isAdmin = currentUser.Claims.ToList().Where(t => t.Type == "admin").FirstOrDefault()!.Value
                    == "True";
            }

            if (!isAdmin) return Forbid();

            var reportedUsers = await _socialNetworkRepository.GetReportedUsersAsync();

            return Ok(_mapper.Map<IEnumerable<ReportUserDto>>(reportedUsers));
        }

        [HttpDelete("DeleteUserReport/{reportedUserId}")]
        public async Task<ActionResult> DeleteUserReport(int reportedUserId)
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

            var userReport = await _socialNetworkRepository.GetReportedUserAsync(reportedUserId);

            if (userReport == null) return NotFound();

            if (userReport.UserId != userId && !isAdmin) return Forbid();

            await _socialNetworkRepository.DeleteUserReportAsync(userReport);

            return NoContent();
        }

        [HttpPost("ReportPost")]
        public async Task<ActionResult> ReportPost(ReportPostForCreationDto reportPostForCreationDto)
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            reportPostForCreationDto.UserId = userId;

            var post = await _socialNetworkRepository
                .GetPostByIdAsync(reportPostForCreationDto.ReportedPostId);

            if (post == null) return NotFound();

            if (reportPostForCreationDto.UserId == post.UserId)
                return BadRequest("You can't report your post.");

            var reportedPost = await _socialNetworkRepository
                .GetReportedPostAsync(reportPostForCreationDto.ReportedPostId);

            if (reportedPost != null) return BadRequest("You already reported this post.");

            var postToReport = _mapper.Map<ReportPost>(reportPostForCreationDto);

            await _socialNetworkRepository.AddPostReportAsync(postToReport);

            return Ok("Post reported successfully.");
        }

        [HttpGet("GetReportedPosts")]
        public async Task<ActionResult<IEnumerable<ReportPostDto>>> GetReportedPosts()
        {
            ClaimsPrincipal currentUser = User;

            bool isAdmin = false;

            if (currentUser.Claims.Any())
            {
                isAdmin = currentUser.Claims.ToList().Where(t => t.Type == "admin").FirstOrDefault()!.Value
                    == "True";
            }

            if (!isAdmin) return Forbid();

            var reportedPost = await _socialNetworkRepository.GetReportedPostsAsync();

            return Ok(_mapper.Map<IEnumerable<ReportPostDto>>(reportedPost));
        }

        [HttpDelete("DeletePostReport/{reportPostId}")]
        public async Task<ActionResult> DeletePostReport(int reportPostId)
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

            var postReport = await _socialNetworkRepository.GetReportedPostAsync(reportPostId);

            if (postReport == null) return NotFound();

            if (postReport.UserId != userId && !isAdmin) return Forbid();

            await _socialNetworkRepository.DeletePostReportAsync(postReport);

            return NoContent();
        }
    }
}
