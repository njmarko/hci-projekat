using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Modals.Interfaces;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Services
{
    public class ModalService : IModalService
    {
        private readonly IServiceProvider _serviceProvider;

        public ModalService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool? ShowModal<T>(ViewModelBase viewModel) where T : IModalWindow
        {
            IModalWindow window = _serviceProvider.GetRequiredService<T>();
            window.DataContext = viewModel;
            ((Window)window).Owner = Application.Current.MainWindow;
            return window.ShowDialog();
        }
    }
}
