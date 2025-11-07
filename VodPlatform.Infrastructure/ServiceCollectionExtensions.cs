using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Infrastructure.Persistence.Contexts;

namespace VodPlatform.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VodPlatformDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<VodPlatformDbContext>()
            .AddDefaultTokenProviders();


            services.AddDbContext<VodPlatformDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("VodPlatform"),
                    b => b.MigrationsAssembly(typeof(VodPlatformDbContext).Assembly.FullName)
                ));

            services.AddScoped<IMovieGroupRepository, MovieGroupRepository>();
            services.AddScoped<ISeriesGroupRepository, SeriesGroupRepository>();
            services.AddScoped<IWatchedListRepository, WatchedListRepository>();
            services.AddScoped<IWatchlistRepository, WatchlistRepository>();

            return services;
        }
    }
}
