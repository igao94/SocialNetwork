using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.Authorization.Constants;

namespace Persistence;

public class Seed
{
    public static async Task SeedDataAsync(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            List<IdentityRole> roles =
            [
                new() { Name = UserRoles.User },
                new() { Name = UserRoles.Admin }
            ];

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        if (!userManager.Users.Any())
        {
            AppUser admin = new()
            {
                FirstName = "Igor",
                LastName = "Milosavljevic",
                UserName = "igor",
                Email = "igor@gmail.com",
                DateOfBirth = new DateOnly(1994, 2, 16)
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");

            await userManager.AddToRoleAsync(admin, UserRoles.Admin);

            List<AppUser> users =
            [
                new ()
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    UserName = "jane",
                    Email = "jane@gmail.com",
                    DateOfBirth = new DateOnly(1985, 9, 28)
                },

                new ()
                {
                    FirstName = "Michael",
                    LastName = "Johnson",
                    UserName = "michael",
                    Email = "michael@gmail.com",
                    DateOfBirth = new DateOnly(1982, 12, 10)
                },

                new ()
                {
                    FirstName = "Emily",
                    LastName = "Brown",
                    UserName = "emily",
                    Email = "emily@gmail.com",
                    DateOfBirth = new DateOnly(1995, 7, 3)
                },

                new ()
                {
                    FirstName = "Daniel",
                    LastName = "Williams",
                    UserName = "daniel",
                    Email = "daniel@gmail.com",
                    DateOfBirth = new DateOnly(1988, 3, 20)
                }
            ];

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");

                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }
    }
}
