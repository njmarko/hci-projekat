using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.CustomAttributes;
using UI.Services.Interfaces;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class UpdateProfileViewModel : ValidationModel<UpdateProfileViewModel>
    {
        public bool CanUpdate => IsValid();


        private User _user;
        public User User
        {
            get { return _user; }
            set 
            { 
                _user = value;
                OnPropertyChanged(nameof(User));
                OnPropertyChanged(nameof(CanUpdate));

            }
        }


        private string _firstName;

       
        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(CanUpdate));

            }
        }

        private string _lastName;

       
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(CanUpdate));

            }
        }

        private string _username;

       
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanUpdate));

            }
        }

        private DateTime _dateOfBirth;

       
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                OnPropertyChanged(nameof(CanUpdate));
            }
        }

        public ICommand Update { get; private set; }






        public ErrorMessageViewModel FirstNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel LastNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel UsernameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel DateOfBirthError { get; private set; } = new ErrorMessageViewModel();

        public UpdateProfileViewModel(IApplicationContext context, IUserService userService) : base(context)
        {
            User = Context.Store.CurrentUser;
            Username = _user.Username;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            DateOfBirth = _user.DateOfBirth;
            Update = new UpdateProfileCommand(this, userService);
        
        }

        public bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Username))
            {
                UsernameError.ErrorMessage = "Username cannot be empty.";
                isValid = false;
            }
            else
            {
                UsernameError.ErrorMessage = null;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                LastNameError.ErrorMessage = "Last name cannot be empty.";
                isValid = false;
            }
            else
            {
                LastNameError.ErrorMessage = null;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                FirstNameError.ErrorMessage = "First name cannot be empty.";
                isValid = false;
            }
            else
            {
                FirstNameError.ErrorMessage = null;
            }
            
            
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            
            if (DateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            if (age < 18)
            {
                DateOfBirthError.ErrorMessage = $"Your age cannot be less than 18.";
                isValid = false;
            }
            else
            {
                DateOfBirthError.ErrorMessage = null;
            }


            if (_user.Username == Username && _user.FirstName == FirstName && _user.LastName == LastName && _user.DateOfBirth == DateOfBirth)
                isValid = false;

            return isValid;
        }
    }
}
