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
    public class ChangePasswordViewModel : ViewModelBase, ISelfValidatingViewModel
    {


        public bool CanChange => IsValid();

        private string _oldPassword;

        public string OldPassword
        {
            get { return _oldPassword; }
            set 
            { 
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
                OnPropertyChanged(nameof(CanChange));
            }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
                OnPropertyChanged(nameof(CanChange));
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanChange));
            }
        }

        public ICommand ChangePassword { get; set; }


        public ErrorMessageViewModel OldPasswordError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel NewPasswordError { get; private set; } = new ErrorMessageViewModel();

        public ErrorMessageViewModel ConfirmPasswordError { get; private set; } = new ErrorMessageViewModel();


        public ChangePasswordViewModel(IApplicationContext context, IUserService userService) : base(context)
        {
            ChangePassword = new ChangePasswordCommand(this, userService);
        }

        public bool IsValid()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(OldPassword))
            {
                OldPasswordError.ErrorMessage = "Old password cannot be empty.";
                valid = false;
            }
            else
            {
                OldPasswordError.ErrorMessage = null;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                NewPasswordError.ErrorMessage = "New password cannot be empty.";
                valid = false;
            }
            else
            {
                NewPasswordError.ErrorMessage = null;
            }
            if(string.IsNullOrEmpty(ConfirmPassword) || ConfirmPassword != NewPassword)
            {
                ConfirmPasswordError.ErrorMessage = "Passwords don't match";
                valid = false;
            }
            else
            {
                ConfirmPasswordError.ErrorMessage = null;
            }




            return valid;
        }
    }
}
