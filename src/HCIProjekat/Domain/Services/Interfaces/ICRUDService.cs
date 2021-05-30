using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ICRUDService<T>
    {
        T Get(int id);

        T Update(T offer);

        void Delete(int id);
    }
}
