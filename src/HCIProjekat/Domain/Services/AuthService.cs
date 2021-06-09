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
    public class AuthService : IAuthService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public AuthService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public User Login(string username, string password)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.Active);
        }
    }
}
