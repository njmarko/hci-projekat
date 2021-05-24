using Domain.Entities;
using Domain.Services.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public AdminService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Admin Create(Admin admin)
        {
            using var context = _dbContextFactory.CreateDbContext();

            // TODO: Ovde bi bolje bilo koristiti asinhrone pozive, al u sustini moze i ovo
            context.Admins.Add(admin);
            context.SaveChanges();
            return admin;
        }
    }
}
