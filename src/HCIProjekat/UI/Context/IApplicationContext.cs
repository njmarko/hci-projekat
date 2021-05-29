using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;
using UI.Context.Routers;
using UI.Context.Stores;

namespace UI.Context
{
    public interface IApplicationContext
    {
        public IRouter Router { get; set; }
        public IStore Store { get; set; }
        public Notifier Notifier { get; set; }
    }
}
