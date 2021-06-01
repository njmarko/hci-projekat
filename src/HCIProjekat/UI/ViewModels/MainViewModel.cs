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
        public NavbarViewModel NavbarVm { get; private set; }

        public MainViewModel(IApplicationContext context, NavbarViewModel navbarVm) : base(context)
        {
            NavbarVm = navbarVm;
            Context.Router.Push("PartnerOffers");
        }
    }
}
