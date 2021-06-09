using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Context.Locators;
using UI.ViewModels;

namespace UI.Context.Routers
{
    public interface IRouter
    {
        public ViewModelBase CurrentViewModel { get; set; }
        public ICommand RouterPushCommand { get; }
        public event Action<ViewModelBase> RouteChanged;
        public T GetRouteParameter<T>(string parameterName);

        void Push(string route);
    }
}
