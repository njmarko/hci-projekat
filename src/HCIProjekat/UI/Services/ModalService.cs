using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Context;
using UI.Modals;
using UI.Modals.Interfaces;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Services
{
    public class ModalService : IModalService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IApplicationContext _context;

        public ModalService(IServiceProvider serviceProvider, IApplicationContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public bool ShowConfirmationDialog(string message)
        {
            var vm = new ConfirmationViewModel(_context, message);
            IModalWindow window = _serviceProvider.GetRequiredService<ConfirmationModal>();
            window.DataContext = vm;
            (window as Window).Owner = Application.Current.MainWindow;
            return (bool) window.ShowDialog();
        }

        public bool ShowModal<T>(ViewModelBase viewModel) where T : IModalWindow
        {
            IModalWindow window = _serviceProvider.GetRequiredService<T>();
            window.DataContext = viewModel;
            (window as Window).Owner = Application.Current.MainWindow;
            return (bool) window.ShowDialog();
        }
    }
}
