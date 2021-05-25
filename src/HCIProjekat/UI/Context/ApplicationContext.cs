using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context.Routers;
using UI.Context.Stores;

namespace UI.Context
{
    public class ApplicationContext : IApplicationContext
    {
        public IRouter Router { get; set; }
        public IStore Store { get; set; }

        public ApplicationContext(IRouter router, IStore store)
        {
            Router = router;
            Store = store;
        }
    }
}
