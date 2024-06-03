using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Repository;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GDPRController : ControllerBase
    {
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IMapper _mapper;

        public GDPRController(ISocialNetworkRepository socialNetworkRepository,
            IMapper mapper)
        {
            _socialNetworkRepository = socialNetworkRepository ??
                throw new ArgumentNullException(nameof(socialNetworkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUserData()
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            var csvContent = await GenerateUserCsv(userId);

            return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", "UserData.csv");
        }

        private async Task<string> GenerateUserCsv(int userId)
        {
            var userData = await _socialNetworkRepository.GetUserAsync(userId);

            var userDataDto = _mapper.Map<UserDto>(userData);

            using (StringWriter writer = new StringWriter())
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true
                };

                using (CsvWriter csvWriter = new CsvWriter(writer, config))
                {
                    csvWriter.WriteHeader<UserDto>();
                    csvWriter.NextRecord();

                    csvWriter.WriteRecord(userDataDto);
                }
                return writer.ToString();
            }
        }

        [HttpGet("GetPosts")]
        public async Task<ActionResult> GetPostData()
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            var csvContent = await GeneratePostCsv(userId);

            return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", "UserData.csv");
        }

        private async Task<string> GeneratePostCsv(int userId)
        {
            var posts = await _socialNetworkRepository.GetPostsAsync(userId);

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            using (StringWriter writer = new StringWriter())
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true
                };

                using (CsvWriter csvWriter = new CsvWriter(writer, config))
                {
                    csvWriter.WriteHeader<PostDto>();
                    csvWriter.NextRecord();

                    csvWriter.WriteRecords(postsDto);
                }
                return writer.ToString();
            }
        }

        [HttpGet("GetConnections")]
        public async Task<ActionResult> GetConnectionsData()
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            var csvContent = await GenerateConnectionsCsv(userId);

            return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", "UserData.csv");
        }

        private async Task<string> GenerateConnectionsCsv(int userId)
        {
            var connections = await _socialNetworkRepository.GetConnectionsAsync(userId);

            var connectionsDto = _mapper.Map<IEnumerable<ConnectionDto>>(connections);

            using (StringWriter writer = new StringWriter())
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true
                };

                using (CsvWriter csvWriter = new CsvWriter(writer, config))
                {
                    csvWriter.WriteHeader<ConnectionDto>();
                    csvWriter.NextRecord();

                    csvWriter.WriteRecords(connectionsDto);
                }
                return writer.ToString();
            }
        }

        [HttpGet("GetComments")]
        public async Task<ActionResult> GetCommentsData()
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            var csvContent = await GenerateCommentCsv(userId);

            return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", "UserData.csv");
        }

        private async Task<string> GenerateCommentCsv(int userId)
        {
            var comments = await _socialNetworkRepository.GetCommentsAsync(userId);

            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

            using (StringWriter writer = new StringWriter())
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true
                };

                using (CsvWriter csvWriter = new CsvWriter(writer, config))
                {
                    csvWriter.WriteHeader<CommentDto>();
                    csvWriter.NextRecord();

                    csvWriter.WriteRecords(commentsDto);
                }
                return writer.ToString();
            }
        }

        [HttpGet("GetLikes")]
        public async Task<ActionResult> GetLikeData()
        {
            ClaimsPrincipal currentUser = User;

            int userId = 0;

            if (currentUser.Claims.Any())
            {
                int.TryParse(currentUser.Claims.FirstOrDefault()!.Value, out userId);
            }

            var csvContent = await GenerateLikeCsv(userId);

            return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", "UserData.csv");
        }

        private async Task<string> GenerateLikeCsv(int userId)
        {
            var likes = await _socialNetworkRepository.GetLikesAsync(userId);

            var likesDto = _mapper.Map<IEnumerable<LikeDto>>(likes);

            using (StringWriter writer = new StringWriter())
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true
                };

                using (CsvWriter csvWriter = new CsvWriter(writer, config))
                {
                    csvWriter.WriteHeader<LikeDto>();
                    csvWriter.NextRecord();

                    csvWriter.WriteRecords(likesDto);
                }
                return writer.ToString();
            }
        }
    }
}
