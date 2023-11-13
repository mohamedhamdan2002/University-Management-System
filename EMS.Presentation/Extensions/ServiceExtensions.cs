

using EMS.DataAccess.Data;
using EMS.DataAccess.Repositories.Contracts;
using EMS.DataAccess.Repositories.Implementation;
using EMS.Services.Contracts;
using EMS.Services.Implementation;
using Microsoft.EntityFrameworkCore;

namespace EMS.Presentation.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("constr"))
            ); 
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services)
            => services.AddScoped<IServiceManager, ServiceManager>();

        

    }
}
