using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.Modals;
using UI.Services.Interfaces;

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
        private readonly IModalService _modalService;
        private readonly IUserService _userService;

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }

        private bool _didOpenNotifications;
        public bool DidOpenNotifications
        {
            get { return _didOpenNotifications; }
            set
            {
                _didOpenNotifications = value;
                if (_didOpenNotifications == false)
                {
                    NotificationViewModel.Update();
                }
            }
        }

        public NotificationViewModel NotificationViewModel { get; private set; }
        public ICommand Logout { get; private set; }
        public ICommand UpdateProfile { get; private set; }
        public ICommand ChangePassword { get; private set; }
        public ICommand OpenNotifications { get; private set; }

        public ObservableCollection<NavbarItemModel> NavbarItems { get; private set; } = new ObservableCollection<NavbarItemModel>();

        public NavbarViewModel(IApplicationContext context, IModalService modalService, IUserService userService, NotificationViewModel notificationsVm) : base(context)
        {
            _modalService = modalService;
            _userService = userService;
            NotificationViewModel = notificationsVm;

            Logout = new LogoutCommand(context);
            UpdateProfile = new DelegateCommand(OpenUpdateProfileInfoModal);
            ChangePassword = new DelegateCommand(OpenChangePasswordModal);
            OpenNotifications = new DelegateCommand(OpenNotificationsView);

            context.Router.RouteChanged += UpdateNavbar;
        }

        private void OpenChangePasswordModal()
        {
            _modalService.ShowModal<ChangePasswordModal>(new ChangePasswordViewModel(Context, _userService));
        }

        private void OpenUpdateProfileInfoModal()
        {
            _modalService.ShowModal<UpdateProfileInfoModal>(new UpdateProfileViewModel(Context, _userService));
        }

        private void OpenNotificationsView()
        {
            Context.Router.Push("Notifications");
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
                    NavbarItems.Add(new NavbarItemModel { Name = "Requests", Route = "AdminRequests", IsSelected = currentVm is AdminRequestsViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                } 
                else if (user is EventPlanner)
                {
                    NavbarItems.Add(new NavbarItemModel { Name = "Home", Route = "EventPlannerHome", IsSelected = currentVm is EventPlannerHomeViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                    NavbarItems.Add(new NavbarItemModel { Name = "Partners", Route = "EventPlannerHome", IsSelected = false, RouterPushCommand = Context.Router.RouterPushCommand });
                    NavbarItems.Add(new NavbarItemModel { Name = "Requests", Route = "EventPlannerRequests", IsSelected = currentVm is EventPlannerRequestsViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                }
                else
                {
                    NavbarItems.Add(new NavbarItemModel { Name = "Home", Route = "ClientRequests", IsSelected = currentVm is ClientRequestsViewModel, RouterPushCommand = Context.Router.RouterPushCommand });
                }
            } 
        }
    }
}
