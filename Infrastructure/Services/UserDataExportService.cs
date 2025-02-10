using Application.Comments.DTOs;
using Application.Followers.DTOs;
using Application.Interfaces;
using Application.Likes.DTOs;
using Application.Photos.DTOs;
using Application.PostReports.DTOs;
using Application.Posts.DTOs;
using Application.UserReports.DTOs;
using Application.Users.DTOs;
using CsvHelper;
using System.Globalization;
using System.IO.Compression;

namespace Infrastructure.Services;

public class UserDataExportService : IUserDataExportService
{
    public async Task<byte[]> GenerateUserDataZipAsync(UserDto userDto,
    List<FollowDto> followDto,
    List<CommentDto> commentsDto,
    List<LikeDto> likesDto,
    List<PhotoDto> photosDto,
    List<PostDto> postsDto,
    List<PostPhotoDto> postPhotosDto,
    List<PostReportDto> postReportsDto,
    List<UserReportDto> userReportsDto)
    {
        var memoryStream = new MemoryStream();

        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            await AddCsvToArchiveAsync(archive, "User.csv", [userDto]);
            await AddCsvToArchiveAsync(archive, "Followings.csv", followDto);
            await AddCsvToArchiveAsync(archive, "Comments.csv", commentsDto);
            await AddCsvToArchiveAsync(archive, "Likes.csv", likesDto);
            await AddCsvToArchiveAsync(archive, "Photos.csv", photosDto);
            await AddCsvToArchiveAsync(archive, "Posts.csv", postsDto);
            await AddCsvToArchiveAsync(archive, "PostPhotos.csv", postPhotosDto);
            await AddCsvToArchiveAsync(archive, "PostReports.csv", postReportsDto);
            await AddCsvToArchiveAsync(archive, "UserReports.csv", userReportsDto);
        }

        return memoryStream.ToArray();
    }

    private static async Task AddCsvToArchiveAsync<T>(ZipArchive archive, string fileName, 
        IEnumerable<T> records)
    {
        var entry = archive.CreateEntry(fileName);

        using var entryStream = entry.Open();

        using var writer = new StreamWriter(entryStream);

        using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

        await csvWriter.WriteRecordsAsync(records);
    }
}
