using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Users.Application.Interfaces;

namespace Users.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IUserDbContext>(provide => provide.GetService<UsersDbContext>());
            return services;
        }
    } 
}
