using Domain.Entities;
using Domain.Exceptions;
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

        public User ChangeUserPassword(int userId, string oldPassword, string newPassword)
        {
            //throw new NotImplementedException();
            using var context = _dbContextFactory.CreateDbContext();
            var user = context.Users.Find(userId);
            if (user.Password != oldPassword)
                throw new InvalidOldPasswordException("");
            user.Password = newPassword;
            context.SaveChanges();
            return user;
        }

        public User GetUser(int userId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Users.Find(userId);
        }

        public User Update(int userId, string username, string firstName, string lastName, DateTime dateOfBirth)
        {
            using var context = _dbContextFactory.CreateDbContext();
            if (context.Users.FirstOrDefault(u => u.Username == username && u.Id != userId) != null)
            {
                throw new UsernameAlreadyExistsException(username);
            }
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
