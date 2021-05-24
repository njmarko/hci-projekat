using Domain.Services;
using Domain.Services.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IAdminService, AdminService>();
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Action<DbContextOptionsBuilder> dbContextOptionsBuilder = options => options.UseSqlite(configuration.GetConnectionString("sqlite"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            //services.AddDbContext<ApplicationDbContext>(dbContextOptionsBuilder);
            services.AddDbContextFactory<ApplicationDbContext>(dbContextOptionsBuilder);
            return services;
        }
    }
}
