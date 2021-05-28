using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class ErrorMessageViewModel : ViewModelBase
    {
        private string _errorMessage = "";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); OnPropertyChanged(nameof(HasError)); }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public ErrorMessageViewModel(IApplicationContext context) : base(context)
        {
        }
    }
}
