using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _dbContextConfiguration;

        public ApplicationDbContextFactory(Action<DbContextOptionsBuilder> dbContextConfiguration)
        {
            _dbContextConfiguration = dbContextConfiguration;
        }

        public IApplicationDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>();
            _dbContextConfiguration(options);
            return new ApplicationDbContext(options.Options);
        }
    }
}
