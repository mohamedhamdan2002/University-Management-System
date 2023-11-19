

using UMS.DataAccess.Data;
using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using UMS.DataAccess.Repositories.Implementation;
using UMS.Services.Contracts;
using UMS.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UMS.Presentation.Extensions
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
        public static void ConfigureIdentity(this IServiceCollection services)
            => services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>();
    }
}
