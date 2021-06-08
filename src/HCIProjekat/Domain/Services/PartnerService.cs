using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PartnerService : IPartnerService
    {

        private readonly IApplicationDbContextFactory _dbContextFactory;

        public PartnerService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public Partner Get(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Partners.Find(id);
        }
    }
}
