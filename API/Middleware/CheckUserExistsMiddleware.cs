using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace API.Middleware;

public class CheckUserExistsMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/Accounts")
            || context.Request.Path.StartsWithSegments("/swagger"))
        {
            await next(context);

            return;
        }

        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        var username = context.User.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(username) || !await dbContext.Users.AnyAsync(u => u.UserName == username
            && u.IsActive))
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            var response = new { Message = "Please log in." };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response.Message, options);

            await context.Response.WriteAsync(json);

            return;
        }

        await next(context);
    }
}
