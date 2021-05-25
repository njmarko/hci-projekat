using Domain.Persistence;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Action<DbContextOptionsBuilder> dbContextOptionsBuilder;
            if (configuration.GetValue<bool>("UseInMemoryDb"))
            {
                dbContextOptionsBuilder = options => options.UseInMemoryDatabase("HCIInMemoryDb");
            }
            else
            {
                dbContextOptionsBuilder = options => options.UseSqlite(configuration.GetConnectionString("sqlite"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
            services.AddDbContext<ApplicationDbContext>(dbContextOptionsBuilder);
            services.AddSingleton<IApplicationDbContextFactory>(new ApplicationDbContextFactory(dbContextOptionsBuilder));
            return services;
        }
    }
}
