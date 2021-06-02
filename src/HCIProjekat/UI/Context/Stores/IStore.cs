using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Context.Stores
{
    public interface IStore
    {
        User CurrentUser { get; set; }

        public event Action<User> CurrentUserChanged;
    }
}
