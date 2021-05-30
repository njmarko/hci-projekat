using Domain.Exceptions;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Context;
using UI.ViewModels;
using ToastNotifications.Messages;


namespace UI.Commands
{
    public class UpdateProfileCommand : ICommand
    {
        private readonly UpdateProfileViewModel _updateProfileVm;
        private readonly IUserService _userService;


        public event EventHandler CanExecuteChanged;

        public UpdateProfileCommand(UpdateProfileViewModel updateProfileVm, IUserService userService)
        {
            _updateProfileVm = updateProfileVm;
            _userService = userService;
            _updateProfileVm.PropertyChanged += _updateProfileVm_PropertyChanged;

        }

        private void _updateProfileVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UpdateProfileViewModel.CanUpdate))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _updateProfileVm.CanUpdate;
        }

        public void Execute(object parameter)
        {
            try
            {
                var user = _userService.Update(_updateProfileVm.User.Id, _updateProfileVm.Username, _updateProfileVm.FirstName ,_updateProfileVm.LastName,_updateProfileVm.DateOfBirth);
                
                _updateProfileVm.Context.Store.CurrentUser = user;
                _updateProfileVm.User = user;
                _updateProfileVm.Username = user.Username;
                _updateProfileVm.FirstName = user.FirstName;
                _updateProfileVm.LastName = user.LastName;
                _updateProfileVm.DateOfBirth = user.DateOfBirth;
                _updateProfileVm.Context.Notifier.ShowSuccess("Profile info successfully updated.");

            }
            catch (UsernameAlreadyExistsException exception)
            {
                _updateProfileVm.UsernameError.ErrorMessage = exception.Message;
            }
        }
    }
}
