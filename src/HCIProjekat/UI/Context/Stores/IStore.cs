using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Context.Stores
{
    public interface IStore
    {
        public int LoggedUserId { get; set; }

    }
}
