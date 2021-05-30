using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Modals.Interfaces;
using UI.ViewModels;

namespace UI.Services.Interfaces
{
    public interface IModalService
    {
        bool? ShowModal<T>(ViewModelBase viewModel) where T : IModalWindow;
        bool? CreateConfirmationDialog(string message);
    }
}
