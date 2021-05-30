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
    public class UserService : IUserService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public UserService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public User GetUser(int userId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Users.Find(userId);
        }

        public User Update(int userId, string username, string firstName, string lastName, DateTime dateOfBirth)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var user = context.Users.Find(userId);
            user.Username = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.DateOfBirth = dateOfBirth;
            context.SaveChanges();
            return user;
        }
    }
}
