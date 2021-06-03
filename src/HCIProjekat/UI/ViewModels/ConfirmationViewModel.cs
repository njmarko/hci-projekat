using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Commands;
using UI.Context;

namespace UI.ViewModels
{
    public class ConfirmationViewModel : ViewModelBase
    {
        private string _confirmMessage;
        public string ConfirmMessage
        {
            get { return _confirmMessage; }
            set { _confirmMessage = value;}
        }

        public ICommand Yes { get; private set; }
        public ICommand No { get; private set; }

        public ConfirmationViewModel(IApplicationContext context, string confirmMessage) : base(context)
        {
            ConfirmMessage = confirmMessage;
            Yes = new YesCommand();
            No = new NoCommand();
        }

    }
}
