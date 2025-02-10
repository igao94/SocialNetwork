using Application.Comments.DTOs;
using Application.Followers.DTOs;
using Application.Likes.DTOs;
using Application.Photos.DTOs;
using Application.PostReports.DTOs;
using Application.Posts.DTOs;
using Application.UserReports.DTOs;
using Application.Users.DTOs;

namespace Application.Interfaces;

public interface IUserDataExportService
{
    Task<byte[]> GenerateUserDataZipAsync(UserDto userDto,
        List<FollowDto> followDto,
        List<CommentDto> commentsDto,
        List<LikeDto> likesDto,
        List<PhotoDto> photosDto,
        List<PostDto> postsDto,
        List<PostPhotoDto> postPhotosDto,
        List<PostReportDto> postReportsDto,
        List<UserReportDto> userReportsDto);
}
