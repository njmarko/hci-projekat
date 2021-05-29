using Domain.Entities;
using Domain.Exceptions;
using Domain.Persistence;
using Domain.Services.Interfaces;
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
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public AdminService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Admin Create(Admin admin)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Users.FirstOrDefault(u => u.Username == admin.Username) != null)
            {
                throw new UsernameAlreadyExistsException(admin.Username);
            }

            context.Admins.Add(admin);
            context.SaveChanges();
            return admin;

        }
    }
}
