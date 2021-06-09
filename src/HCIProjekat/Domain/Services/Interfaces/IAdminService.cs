using Domain.Entities;
using Domain.Pagination;
using Domain.Pagination.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IAdminService : ICRUDService<Admin>
    {
        Admin Create(Admin admin);

        Page<Admin> GetAdmins(AdminsPage page);


    }
}
