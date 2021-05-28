using Domain.Entities;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Context.Routers;
using UI.ViewModels;

namespace UI.Commands
{
    public class RegisterAdminCommand : ICommand
    {
        private readonly RegisterAdminViewModel _registerVm;
        private readonly IAdminService _adminService;
        private readonly IRouter _router;

        public event EventHandler CanExecuteChanged;

        public RegisterAdminCommand(RegisterAdminViewModel registerVm, IAdminService adminService, IRouter router)
        {
            _registerVm = registerVm;
            _adminService = adminService;
            _router = router;
            _registerVm.PropertyChanged += _registerVm_PropertyChanged;
        }

        private void _registerVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegisterAdminViewModel.CanRegister))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _registerVm.CanRegister;
        }

        public void Execute(object parameter)
        {
            try
            {
                var registerAdmin = _adminService.Create(new Admin { FirstName = _registerVm.FirstName, LastName = _registerVm.LastName, Password = _registerVm.Password, Username = _registerVm.Username, DateOfBirth = _registerVm.DateOfBirth });
                // Just a message to show it works. Success message will be changed after the windows are connected it also redirects to aprtners for now. This should be changed
                // TODO: Change message displaying for successfull registration
                MessageBox.Show($"Admin sucessfuly added, {registerAdmin.FirstName} {registerAdmin.LastName}.");
                _router.Push("AdminPartners");
            }
            catch (UsernameAlreadyExistsException exception)
            {
                _registerVm.UsernameError.ErrorMessage = exception.Message;
            }
        }
    }
}
