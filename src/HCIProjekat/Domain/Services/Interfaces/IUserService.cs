using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {

        User Update(int userId, string username, string firstName, string lastName, DateTime dateOfBirth);

        User GetUser(int userId);
    }
}
