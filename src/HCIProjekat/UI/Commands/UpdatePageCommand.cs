using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels.Interfaces;

namespace UI.Commands
{
    public class UpdatePageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IPagingViewModel _vm;

        public UpdatePageCommand(IPagingViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (int.TryParse(parameter.ToString(), out int pageNumber)) {
                _vm.UpdatePage(pageNumber);
            }
        }
    }
}
