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
    public class RegisterViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private DateTime _dateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); OnPropertyChanged(nameof(CanRegister)); }
        }

        public bool CanRegister =>
            !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && (Password == ConfirmPassword);
        public ICommand RegisterCommand { get; private set; }

        public RegisterViewModel(IApplicationContext context, IClientService clientService) : base(context)
        {
            RegisterCommand = new RegisterCommand(this, clientService, context.Router);
        }
    }
}
