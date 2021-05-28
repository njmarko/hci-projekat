using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class RegisterEventPlannerViewModel : ViewModelBase, ISelfValidatingViewModel
    {
        // Poperties
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

        // Error message view models
        public ErrorMessageViewModel FirstNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel LastNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel UsernameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel PasswordError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel ConfirmPasswordError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel DateOfBirthError { get; private set; } = new ErrorMessageViewModel();

        public bool CanRegister => IsValid();
        public ICommand RegisterEventPlannerCommand { get; private set; }

        public RegisterEventPlannerViewModel(IApplicationContext context, IEventPlannersService eventPlannerService) : base(context)
        {
            //DateOfBirth = new DateTime(1990, 01, 01);
            RegisterEventPlannerCommand = new RegisterEventPlannerCommand(this, eventPlannerService, context.Router);
        }

        public bool IsValid()
        {
            bool valid = true;

            //Username
            if (string.IsNullOrEmpty(Username))
            {
                UsernameError.ErrorMessage = "Username cannot be empty.";
                valid = false;
            }
            else
            {
                UsernameError.ErrorMessage = null;
            }
            //First name
            if (string.IsNullOrEmpty(FirstName))
            {
                FirstNameError.ErrorMessage = "First name cannot be empty.";
                valid = false;
            }
            else
            {
                FirstNameError.ErrorMessage = null;
            }
            //Last name
            if (string.IsNullOrEmpty(LastName))
            {
                LastNameError.ErrorMessage = "Last name cannot be empty.";
                valid = false;
            }
            else
            {
                LastNameError.ErrorMessage = null;
            }
            //Password
            if (string.IsNullOrEmpty(Password))
            {
                PasswordError.ErrorMessage = "Password cannot be empty.";
                valid = false;
            }
            else
            {
                PasswordError.ErrorMessage = null;
            }
            //Confirm password
            if (ConfirmPassword != Password)
            {
                ConfirmPasswordError.ErrorMessage = "Password and confirm password must match.";
                valid = false;
            }
            else
            {
                ConfirmPasswordError.ErrorMessage = null;
            }
            //Date of birth
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            if (age < 18)
            {
                DateOfBirthError.ErrorMessage = "You must be at least 18 years old in order to register.";
                valid = false;
            }
            else
            {
                DateOfBirthError.ErrorMessage = null;
            }
            return valid;
        }
    }
}
