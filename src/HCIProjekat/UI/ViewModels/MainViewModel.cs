using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IApplicationContext context) : base(context)
        {
            Context.Router.Push("RegisterAdmin");
        }
    }
}
