using Domain.Entities;
using Domain.Exceptions;
using Domain.Pagination;
using Domain.Pagination.Requests;
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

        public void Delete(int adminId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var admin = context.Admins.Find(adminId);
            admin.Active = false;
            context.SaveChanges();
        }

        public Page<Admin> GetAdmins(AdminsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Admins
                          .Where(c => c.Active)
                          .Where(c => c.Username.ToLower().Contains(page.Query.ToLower())
                          || c.FirstName.ToLower().Contains(page.Query.ToLower())
                          || c.LastName.ToLower().Contains(page.Query.ToLower()))
                          .Where(c => c.DateOfBirth <= page.BornBefore || !page.BornBefore.HasValue)
                          .Where(c => c.DateOfBirth >= page.BornAfter || !page.BornAfter.HasValue)
                          .ToPage(page);
        }
    }
}
