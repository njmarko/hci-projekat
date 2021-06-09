using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Context;

namespace UI.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IApplicationContext _context;
        public event EventHandler CanExecuteChanged;

        public LogoutCommand(IApplicationContext context)
        {
            _context = context;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _context.Store.CurrentUser = null;
            _context.Router.Push("Login");
        }
    }
}
