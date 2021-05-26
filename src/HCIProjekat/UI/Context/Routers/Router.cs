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

        public ViewModelBase CurrentViewModel { get; set; }

        public ICommand RouterPushCommand { get; private set; } 

        public Router(IViewModelLocator locator)
        {
            _locator = locator;
            RouterPushCommand = new RedirectCommand(this);
        }

        public void Push(string route)
        {
            CurrentViewModel = route switch
            {
                "Login" => _locator.Get<LoginViewModel>(),
                "ClientRequests" => _locator.Get<ClientRequestsViewModel>(),
                _ => throw new Exception($"Undefined route '{route}'. No view model registered for the given route.")
            };
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));
        }
    }
}
