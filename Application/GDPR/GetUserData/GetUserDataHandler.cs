using Application.Comments.DTOs;
using Application.Core;
using Application.Followers.DTOs;
using Application.Interfaces;
using Application.Likes.DTOs;
using Application.Photos.DTOs;
using Application.PostReports.DTOs;
using Application.Posts.DTOs;
using Application.UserReports.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GDPR.GetUserData;

public class GetUserDataHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IUserDataExportService userDataExportService,
    IMapper mapper) : IRequestHandler<GetUserDataQuery, Result<byte[]>?>
{
    public async Task<Result<byte[]>?> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosAndPostsAndPostPhotosAndFollowByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var postsQuery = unitOfWork.PostsRepository.GetAllPostsQuery();

        var postsPhotos = unitOfWork.PostsRepository.GetPostPhotos(user);

        var commentsQuery = unitOfWork.CommentsRepository.GetAllCommentsQuery(user.Id);

        var likesQuery = unitOfWork.LikesRepository.GetAllUserLikesQuery(user.Id);

        var photosQuery = unitOfWork.PhotosRepository.GetAllUserPhotosQuery(user);

        var followQuery = unitOfWork.FollowingsRepository.GetAllUserFollowsQuery(user.Id);

        var postsReportsQuery = unitOfWork.ReportsRepository.GetPostsReportsForUserQuery(user.Id);

        var usersReportsQuery = unitOfWork.ReportsRepository.GetUsersReportsForUserQuery(user.Id);

        var userDto = mapper.Map<UserDto>(user);

        var postsDto = await postsQuery.ProjectTo<PostDto>(mapper.ConfigurationProvider).ToListAsync();

        var postsPhotosDto = mapper.Map<List<PostPhotoDto>>(postsPhotos);

        var commentsDto = await commentsQuery.ProjectTo<CommentDto>(mapper.ConfigurationProvider).ToListAsync();

        var likesDto = await likesQuery.ProjectTo<LikeDto>(mapper.ConfigurationProvider).ToListAsync();

        var photosDto = photosQuery.ProjectTo<PhotoDto>(mapper.ConfigurationProvider).ToList();

        var followDto = await followQuery.ProjectTo<FollowDto>(mapper.ConfigurationProvider).ToListAsync();

        var postsReportsDto = await postsReportsQuery
            .ProjectTo<PostReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        var usersReportsDto = await usersReportsQuery
            .ProjectTo<UserReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        var zipFileContent = await userDataExportService.GenerateUserDataZipAsync(userDto,
            followDto,
            commentsDto,
            likesDto,
            photosDto,
            postsDto,
            postsPhotosDto,
            postsReportsDto,
            usersReportsDto);

        return Result<byte[]>.Success(zipFileContent);
    }
}
