using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;

namespace UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); OnPropertyChanged(nameof(CanLogin)); ErrorViewModel.ErrorMessage = null; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); OnPropertyChanged(nameof(CanLogin)); ; ErrorViewModel.ErrorMessage = null; }
        }

        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        public ErrorMessageViewModel ErrorViewModel { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(IApplicationContext context, IAuthService authService) : base(context)
        {
            ErrorViewModel = new ErrorMessageViewModel(context);
            LoginCommand = new LoginCommand(this, authService, context);
        }
    }
}
