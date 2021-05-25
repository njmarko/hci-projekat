using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _handler;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action handler)
        {
            _handler = handler;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _handler();
        }
    }
}
