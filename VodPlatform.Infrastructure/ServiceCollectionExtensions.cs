using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VodPlatform.Infrastructure.Persistence.Contexts;
using VodPlatform.Core.Domain.Repositories;

namespace VodPlatform.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
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
