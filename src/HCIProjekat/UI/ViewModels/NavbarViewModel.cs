using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Context;

namespace UI.ViewModels
{
    public class NavbarItemModel
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public bool IsSelected { get; set; }
        public ICommand RouterPushCommand { get; set; }
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
                    NavbarItems.Add(new NavbarItemModel { Name = "Partners", Route = "AdminPartners", IsSelected = currentVm is AdminPartnersViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                    NavbarItems.Add(new NavbarItemModel { Name = "Event planners", Route = "AdminEventPlanners", IsSelected = currentVm is AdminEventPlannersViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                    NavbarItems.Add(new NavbarItemModel { Name = "Clients", Route = "AdminClients", IsSelected = currentVm is AdminClientsViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                } 
                else if (user is EventPlanner)
                {
                    NavbarItems.Add(new NavbarItemModel { Name = "Home", Route = "EventPlannerHome", IsSelected = currentVm is EventPlannerHomeViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                    NavbarItems.Add(new NavbarItemModel { Name = "Partners", Route = "EventPlannerHome", IsSelected = false, RouterPushCommand = Context.Router.RouterPushCommand });
                    NavbarItems.Add(new NavbarItemModel { Name = "Requests", Route = "EventPlannerHome", IsSelected = false, RouterPushCommand = Context.Router.RouterPushCommand });
                }
                else
                {
                    NavbarItems.Add(new NavbarItemModel { Name = "Home", Route = "ClientRequests", IsSelected = currentVm is ClientRequestsViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                }
            }
        }
    }
}
