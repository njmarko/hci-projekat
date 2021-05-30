using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels.CardViewModels
{
    public class ClientRequestCardModel
    {
        public string Name { get; set; }

        public IApplicationContext Context { get; set; }

    }
}
