using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context.Locators;
using UI.ViewModels;

namespace UI.Context.Routers
{
    public class Router : IRouter, INotifyPropertyChanged
    {
        private readonly IViewModelLocator _locator;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<ViewModelBase> OnRouteChanged;

        public ViewModelBase CurrentViewModel { get; set; }

        public ICommand RouterPushCommand { get; private set; } 

        public Router(IViewModelLocator locator)
        {
            _locator = locator;
            RouterPushCommand = new RedirectCommand(this);
        }

        public void Push(string route)
        {
            /*CurrentViewModel = route switch
            {
                "Login" => _locator.Get<LoginViewModel>(),
                "Register" => _locator.Get<RegisterViewModel>(),
                "ClientRequests" => _locator.Get<ClientRequestsViewModel>(),
                "AdminClients" => _locator.Get<AdminClientsViewModel>(),
                "AdminEventPlanners" => _locator.Get<AdminEventPlannersViewModel>(),
                "AdminPartners" => _locator.Get<AdminPartnersViewModel>(),
                "RegisterEventPlanner" => _locator.Get<RegisterEventPlannerViewModel>(),
                "RegisterAdmin" => _locator.Get<RegisterAdminViewModel>(),
                "RegisterPartner" => _locator.Get<RegisterPartnerViewModel>(),
                "RequestDetails" => _locator.Get<RequestDetailsViewModel>(),
                "TaskDetails" => _locator.Get<TaskDetailsViewModel>(),
                "EventPlannerHome" => _locator.Get<EventPlannerHomeViewModel>(),
                "PartnerOffers" => _locator.Get<PartnerOffersViewModel>(),
                _ => throw new Exception($"Undefined route '{route}'. No view model registered for the given route.")
            };*/
            OnRouteChanged?.Invoke(CurrentViewModel);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));

            ViewModelBase viewModel;


            if (route.Contains("Login"))
            {
                viewModel = _locator.Get<LoginViewModel>();

            }
            else if (route.Contains("Register"))
            {
                viewModel = _locator.Get<RegisterViewModel>();
            }
            else if (route.Contains("ClientRequests"))
            {
                viewModel = _locator.Get<ClientRequestsViewModel>();
            }
            else if (route.Contains("AdminClients"))
            {
                viewModel = _locator.Get<AdminClientsViewModel>();
            }
            else if (route.Contains("AdminEventPlanners"))
            {
                viewModel = _locator.Get<AdminEventPlannersViewModel>();
            }
            else if (route.Contains("RegisterEventPlanner"))
            {
                viewModel = _locator.Get<RegisterEventPlannerViewModel>();
            }
            else if (route.Contains("RegisterAdmin"))
            {
                viewModel = _locator.Get<RegisterAdminViewModel>();
            }
            else if (route.Contains("RegisterPartner"))
            {
                viewModel = _locator.Get<RegisterPartnerViewModel>();
            }
            else if (route.Contains("RequestDetails"))
            {
                viewModel = _locator.Get<RequestDetailsViewModel>();
                string[] tokens = route.Split("?");
                string idValue = tokens[1].Split("=")[1];
                ((RequestDetailsViewModel)viewModel).RequestId = Int32.Parse(idValue);
            }
            else if (route.Contains("TaskDetails"))
            {
                viewModel = _locator.Get<TaskDetailsViewModel>();
                string[] tokens = route.Split("?");
                string idValue = tokens[1].Split("=")[1];
                ((TaskDetailsViewModel)viewModel).TaskId = Int32.Parse(idValue);
            }
            else if (route.Contains("EventPlannerHome"))
            {
                viewModel = _locator.Get<EventPlannerHomeViewModel>();
            }
            else if (route.Contains("PartnerOffers"))
            {
                viewModel = _locator.Get<PartnerOffersViewModel>();
            }
            else
            {
                throw new Exception($"Undefined route '{route}'. No view model registered for the given route.");
            }
            
            
            CurrentViewModel = viewModel;
            OnRouteChanged?.Invoke(CurrentViewModel);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));

        }
    }
}
