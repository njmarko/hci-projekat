using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Context.Routers;

namespace UI.Commands
{
    public class RedirectCommand : ICommand
    {
        private readonly IRouter _router;
        public event EventHandler CanExecuteChanged;

        public RedirectCommand(IRouter router)
        {
            _router = router;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is string stringParam)
            {
                _router.Push(stringParam);
            }
        }
    }
}
