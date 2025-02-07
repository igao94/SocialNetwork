using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Data;
using Application.Accounts.Register;
using Application.Core;
using Domain.Interfaces;
using Persistence.Repositories;
using Infrastructure;
using Infrastructure.Services.Photos;
using Application.Interfaces;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            opt.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("SocialNetworkBearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid token to access this API."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "SocialNetworkBearerAuth"
                        }
                    }, []
                }
            });
        });

        var userInMemoryDatabase = config.GetValue<bool>("UseInMemoryDatabase");

        services.AddDbContext<DataContext>(opt =>
        {
            if (userInMemoryDatabase)
            {
                opt.UseInMemoryDatabase("InMemoryDatabase");
            }
            else
            {
                opt.UseSqlServer(config.GetConnectionString("SqlServer"));
            }
        });

        services.AddScoped<IAccountsRepository, AccountsRepository>();

        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<IPhotosRepository, PhotosRepository>();

        services.AddScoped<IPostsRepository, PostsRepository>();

        services.AddScoped<ILikesRepository, LikesRepository>();

        services.AddScoped<ICommentsRepository, CommentsRepository>();

        services.AddScoped<IFollowingsRepository, FollowingsRepository>();

        services.AddScoped<IUserReportsRepository, UserReportsRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));

        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserAccessor, UserAccessor>();

        services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

        services.AddScoped<IPhotosService, PhotosService>();

        return services;
    }
}
