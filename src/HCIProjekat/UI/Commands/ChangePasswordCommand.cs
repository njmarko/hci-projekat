using Domain.Exceptions;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;
using ToastNotifications.Messages;
using System.Windows;

namespace UI.Commands
{
    public class ChangePasswordCommand : ICommand
    {
        private readonly ChangePasswordViewModel _changePasswordVM;
        private readonly IUserService _userService;
        public event EventHandler CanExecuteChanged;

        public ChangePasswordCommand(ChangePasswordViewModel changePasswordVM, IUserService userService)
        {
            _changePasswordVM = changePasswordVM;
            _userService = userService;
            _changePasswordVM.PropertyChanged += _changePassword_PropertyChanged;
        }




        private void _changePassword_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChangePasswordViewModel.CanChange))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }
        public bool CanExecute(object parameter)
        {
            return _changePasswordVM.CanChange;
        }

        public void Execute(object parameter)
        {
            try
            {
                var user = _userService.ChangeUserPassword(_changePasswordVM.Context.Store.CurrentUser.Id, _changePasswordVM.OldPassword, _changePasswordVM.NewPassword);
                _changePasswordVM.OldPassword = "";
                _changePasswordVM.NewPassword = "";
                _changePasswordVM.ConfirmPassword = "";
                _changePasswordVM.Context.Notifier.ShowSuccess("Password successfully changed.");
                //((Window)parameter).Close(); nesto ne moze, javlja null pointer exception


            }
            catch (InvalidOldPasswordException e)
            {
                _changePasswordVM.OldPasswordError.ErrorMessage = e.Message;
            }

        }
    }
}
