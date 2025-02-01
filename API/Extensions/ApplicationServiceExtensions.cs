using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

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

        return services;
    }
}
