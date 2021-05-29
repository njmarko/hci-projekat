using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class NavbarItemModel
    {
        public string Name { get; set; }
        public string Route { get; set; }
    }

    public class NavbarViewModel : ViewModelBase
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }
        public ObservableCollection<NavbarItemModel> NavbarItems { get; private set; } = new ObservableCollection<NavbarItemModel>();

        public NavbarViewModel(IApplicationContext context) : base(context)
        {
            context.Router.OnRouteChanged += UpdateNavbar;
        }

        private void UpdateNavbar(ViewModelBase currentVm)
        {
            IsVisible = (currentVm is not LoginViewModel) && (currentVm is not RegisterViewModel);
            if (IsVisible)
            {
                NavbarItems.Clear();
                User user = Context.Store.CurrentUser;
                if (user is Admin)
                {
                    NavbarItems.Add(new NavbarItemModel { Name = "Admin", Route = "" });
                } 
                else if (user is EventPlanner)
                {
                    NavbarItems.Add(new NavbarItemModel { Name = "Event planner", Route = "" });
                }
                else
                {
                    NavbarItems.Add(new NavbarItemModel { Name = "User", Route = "" });
                }
            }
        }
    }
}
