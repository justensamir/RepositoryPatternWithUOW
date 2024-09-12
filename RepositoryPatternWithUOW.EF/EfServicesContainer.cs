using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.EF.Contexts;
using RepositoryPatternWithUOW.EF.UnitOfWorks;

namespace RepositoryPatternWithUOW.EF
{
    public static class EfServicesContainer
    {
        public static IServiceCollection AddEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
