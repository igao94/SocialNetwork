using SocialNetwork.API.Data;

namespace SocialNetwork.API.Services
{
    public class SeedHelper : ISeedHelper
    {
        private readonly AppDbContext _context;

        public SeedHelper(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Seed()
        {
            if (!_context.Users.Any())
            {
                await _context.Users.AddAsync(
                    new()
                    {
                        UserId = 1,
                        FirstName = "Igor",
                        LastName = "Milosavljevic",
                        Email = "igor@gmail.com",
                        Password = "123456",
                        IsAdmin = true,
                        CreationDate = DateTime.UtcNow
                    });
            }

            await _context.SaveChangesAsync();
        }
    }
}
