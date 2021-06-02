using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.CustomAttributes;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class LoginViewModel : ValidationModel<LoginViewModel>
    {
        private string _username;
        [ValidationField]
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); OnPropertyChanged(nameof(CanLogin)); }
        }

        private string _password;
        [ValidationField]
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); OnPropertyChanged(nameof(CanLogin)); }
        }

        public bool CanLogin => IsValid();
        public ErrorMessageViewModel LoginError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel UsernameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel PasswordError { get; private set; } = new ErrorMessageViewModel();
        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(IApplicationContext context, IAuthService authService) : base(context)
        {
            LoginCommand = new LoginCommand(this, authService, context);
        }

        public bool IsValid()
        {
            bool valid = true;
            //Username
            if (string.IsNullOrEmpty(Username) && IsDirty(nameof(Username)))
            {
                UsernameError.ErrorMessage = "Username cannot be empty.";
                valid = false;
            } else
            {
                UsernameError.ErrorMessage = null;
            }
            //Password
            if (string.IsNullOrEmpty(Password) && IsDirty(nameof(Password)))
            {
                PasswordError.ErrorMessage = "Password cannot be empty.";
                valid = false;
            } else
            {
                PasswordError.ErrorMessage = null;
            }
            LoginError.ErrorMessage = null;
            return valid && AllDirty();
        }
    }
}
