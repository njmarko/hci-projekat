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
using ToastNotifications.Messages;
using UI.Context;
using UI.Context.Routers;
using UI.ViewModels;

namespace UI.Commands
{
    public class RegisterAdminCommand : ICommand
    {
        private readonly RegisterAdminViewModel _registerVm;
        private readonly IAdminService _adminService;
        private readonly IRouter _router;
        private readonly IApplicationContext _context;
        private readonly AdminAdminsViewModel _adminAdminsVm;

        public event EventHandler CanExecuteChanged;

        public RegisterAdminCommand(RegisterAdminViewModel registerVm, IAdminService adminService, IRouter router, IApplicationContext context, AdminAdminsViewModel adminAdminsVm)
        {
            _registerVm = registerVm;
            _adminService = adminService;
            _router = router;
            _context = context;
            _registerVm.PropertyChanged += _registerVm_PropertyChanged;
            _adminAdminsVm = adminAdminsVm;
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
                _context.Notifier.ShowInformation($"Admin {registerAdmin.FirstName} {registerAdmin.LastName} sucessfuly added.");
                // undo redo
                registerAdmin.Active = false;
                _adminAdminsVm.AddItem(registerAdmin);

                _registerVm.ResetFields();
                _adminAdminsVm.UpdatePage(_adminAdminsVm.PaginationViewModel.Page);
            }
            catch (UsernameAlreadyExistsException exception)
            {
                _registerVm.UsernameError.ErrorMessage = exception.Message;
            }
        }
    }
}
