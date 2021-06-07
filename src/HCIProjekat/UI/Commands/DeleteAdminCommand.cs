﻿using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Commands
{
    public class DeleteAdminCommand : ICommand
    {
        private readonly AdminAdminsViewModel _adminAdminsViewModel;
        private readonly int _adminId;
        private readonly string _adminUsername;
        private readonly IModalService _modalService;
        private readonly IAdminService _adminService;

        public event EventHandler CanExecuteChanged;

        public DeleteAdminCommand(AdminAdminsViewModel adminAdminsVm, int adminId, string adminUsername, IModalService modalService, IAdminService adminService)
        {
            _adminAdminsViewModel = adminAdminsVm;
            _adminId = adminId;
            _adminUsername = adminUsername;
            _modalService = modalService;
            _adminService = adminService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_modalService.ShowConfirmationDialog($"Are you sure you want to delete admin {_adminUsername}?"))
            {
                _adminService.Delete(_adminId);
                _adminAdminsViewModel.UpdatePage(0);
                _adminAdminsViewModel.Context.Notifier.ShowInformation($"Admin {_adminUsername} has been deleted successfully.");
            }
        }
    }
}
