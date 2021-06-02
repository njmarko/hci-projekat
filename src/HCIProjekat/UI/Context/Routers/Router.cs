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
        private readonly Dictionary<string, object> _routeParameters = new Dictionary<string, object>();

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
            string baseRoute = ParseRouteParameters(route);
            CurrentViewModel = baseRoute switch
            {
                // No route parameters
                "Login" => _locator.Get<LoginViewModel>(),
                "Register" => _locator.Get<RegisterViewModel>(),
                "ClientRequests" => _locator.Get<ClientRequestsViewModel>(),
                "AdminClients" => _locator.Get<AdminClientsViewModel>(),
                "AdminEventPlanners" => _locator.Get<AdminEventPlannersViewModel>(),
                "AdminPartners" => _locator.Get<AdminPartnersViewModel>(),
                "RegisterEventPlanner" => _locator.Get<RegisterEventPlannerViewModel>(),
                "RegisterAdmin" => _locator.Get<RegisterAdminViewModel>(),
                "RegisterPartner" => _locator.Get<RegisterPartnerViewModel>(),
                "EventPlannerHome" => _locator.Get<EventPlannerHomeViewModel>(),
                "EventPlannerRequests" => _locator.Get<EventPlannerRequestsViewModel>(),

                // With route parameters
                "RequestDetails" => GetRequestDetailsVm(),
                "TaskDetails" => GetTaskDetailsVm(),
                "PartnerOffers" => GetPartnerOffersVm(),
                _ => throw new Exception($"Undefined route '{route}'. No view model registered for the given route.")
            };
            OnRouteChanged?.Invoke(CurrentViewModel);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));
        }

        public T GetRouteParameter<T>(string parameterName)
        {
            if (!_routeParameters.ContainsKey(parameterName))
            {
                throw new Exception($"Undefined route parameter '{parameterName}'.");
            }
            return (T) Convert.ChangeType(_routeParameters[parameterName], typeof(T));
        }

        // TODO: Ovo najverovatnije ispaviti da se u view modelu hvata event kad se promeni route parametar, ali i ovo radi
        private ViewModelBase GetRequestDetailsVm()
        {
            var vm = _locator.Get<RequestDetailsViewModel>() as RequestDetailsViewModel;
            vm.RequestId = GetRouteParameter<int>("requestId");
            return vm;
        }

        private ViewModelBase GetTaskDetailsVm()
        {
            var vm = _locator.Get<TaskDetailsViewModel>() as TaskDetailsViewModel;
            vm.TaskId = GetRouteParameter<int>("taskId");
            return vm;
        }

        private ViewModelBase GetPartnerOffersVm()
        {
            var vm = _locator.Get<PartnerOffersViewModel>() as PartnerOffersViewModel;
            vm.PartnerId = GetRouteParameter<int>("partnerId");
            return vm;
        }

        private string ParseRouteParameters(string route)
        {
            _routeParameters.Clear();
            var tokens = route.Split("?");
            var baseRoute = tokens[0];
            foreach (var param in tokens[1..])
            {
                var paramTokens = param.Split("=");
                _routeParameters[paramTokens[0].Trim()] = paramTokens[1].Trim();
            }
            return baseRoute;
        }
    }
}
