using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Models.DTO;
using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Repository
{
    public class SocialNetworkRepository : ISocialNetworkRepository
    {
        private readonly AppDbContext _context;

        public SocialNetworkRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User?> GetUserByCredentialsAsync(string username, string password)
        {
            return await _context.Users
                .Where(u => u.Email == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<string> CreateTokenAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);

            var token = Guid.NewGuid().ToString();

            if (user != null)
            {
                await _context.ResetPassword.AddAsync(
                    new()
                    {
                        UserId = user.UserId,
                        Token = token,
                        CreationDate = DateTime.UtcNow
                    });

                await _context.SaveChangesAsync();

                return token;
            }

            return "";
        }

        public async Task<int> GetUserIdByTokenAsync(string token)
        {
            var result = await _context.ResetPassword
                .Where(r => r.Token == token && r.CreationDate >= DateTime.UtcNow.AddHours(-24))
                .OrderBy(r => r.CreationDate)
                .LastOrDefaultAsync();

            if (result == null) return 0;

            return result.UserId;
        }

        public async Task<User?> SetPasswordAsync(int userId, string password)
        {
            var user = await GetUserByIdAsync(userId);

            if (user == null) return null;

            user.Password = password;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Where(c => c.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            var user = await _context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                user.Posts = await GetPostsAsync(userId);
                user.Connections = await GetConnectionsAsync(userId);
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            IQueryable<User> collection = _context.Users;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(u => u.FirstName.Contains(searchQuery) ||
                u.LastName.Contains(searchQuery) || u.Email.Contains(searchQuery));
            }

            return await collection.OrderBy(u => u.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> UserEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            user.CreationDate = DateTime.UtcNow;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(int userId)
        {
            var posts = await _context.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();

            foreach (var post in posts)
            {
                post.Comments = await _context.Comments.Where(c => c.PostId == post.PostId).ToListAsync();
                post.Likes = await _context.Likes.Where(l => l.PostId == post.PostId).ToListAsync();
            }

            return posts;
        }

        public async Task<Post?> GetPostAsync(int userId, int postId)
        {
            var post = await _context.Posts
                .Where(p => p.UserId == userId && p.PostId == postId)
                .FirstOrDefaultAsync();

            if (post != null)
            {
                post.Comments = await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
                post.Likes = await _context.Likes.Where(l => l.PostId == postId).ToListAsync();
            }

            return post;
        }

        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            return await _context.Posts.Where(p => p.PostId == postId).FirstOrDefaultAsync();
        }

        public async Task AddPostAsync(Post post)
        {
            post.CreationDate = DateTime.UtcNow;
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            comment.CreationDate = DateTime.UtcNow;
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments.Where(c => c.CommentId == commentId).FirstOrDefaultAsync();
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task AddLikeAsync(Like like)
        {
            like.CreationDate = DateTime.UtcNow;
            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task<Like?> GetLikeByIdAsync(int likeId)
        {
            return await _context.Likes.Where(l => l.LikeId == likeId).FirstOrDefaultAsync();
        }

        public async Task DeleteLikeAsync(Like like)
        {
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<Connection?> GetConnectionAsync(int firstUserId, int secondUserId)
        {
            return await _context.Connections
                .Where(c => c.FirstUserId == firstUserId && c.SecondUserId == secondUserId ||
                    c.FirstUserId == secondUserId && c.SecondUserId == firstUserId)
                .FirstOrDefaultAsync();
        }

        public async Task AddConnectionAsync(int firstUserId, int secondUserId)
        {
            await _context.Connections.AddAsync(
                new()
                {
                    CreationDate = DateTime.UtcNow,
                    FirstUserId = firstUserId,
                    SecondUserId = secondUserId
                });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Connection>> GetConnectionsAsync(int userId)
        {
            return await _context.Connections
                .Where(c => c.FirstUserId == userId || c.SecondUserId == userId)
                .ToListAsync();
        }

        public async Task DeleteConnectionAsync(Connection connection)
        {
            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();
        }

        public async Task<ReportUser?> GetReportedUserAsync(int reportedUserId)
        {
            return await _context.ReportUsers.Where(r => r.ReportedUserId == reportedUserId).FirstOrDefaultAsync();
        }

        public async Task AddUserReportAsync(ReportUser reportUser)
        {
            reportUser.CreationDate = DateTime.UtcNow;
            await _context.ReportUsers.AddAsync(reportUser);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReportUser>> GetReportedUsersAsync()
        {
            return await _context.ReportUsers.ToListAsync();
        }

        public async Task DeleteUserReportAsync(ReportUser reportUser)
        {
            _context.ReportUsers.Remove(reportUser);
            await _context.SaveChangesAsync();
        }

        public async Task<ReportPost?> GetReportedPostAsync(int reportedPostId)
        {
            return await _context.ReportPosts.Where(r => r.ReportedPostId == reportedPostId).FirstOrDefaultAsync();
        }

        public async Task AddPostReportAsync(ReportPost reportPost)
        {
            reportPost.CreationDate = DateTime.UtcNow;
            await _context.ReportPosts.AddAsync(reportPost);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReportPost>> GetReportedPostsAsync()
        {
            return await _context.ReportPosts.ToListAsync();
        }

        public async Task DeletePostReportAsync(ReportPost reportPost)
        {
            _context.ReportPosts.Remove(reportPost);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(int userId)
        {
            return await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Like>> GetLikesAsync(int userId)
        {
            return await _context.Likes.Where(l => l.UserId == userId).ToListAsync();
        }
    }
}
