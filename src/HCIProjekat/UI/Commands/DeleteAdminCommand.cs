using Domain.Services.Interfaces;
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
                var admin = _adminService.Get(_adminId);
                _adminAdminsViewModel.AddItem(admin);
                _adminService.Delete(_adminId);
                if (_adminAdminsViewModel.PaginationViewModel.TotalElements - _adminAdminsViewModel.PaginationViewModel.Page * _adminAdminsViewModel.PaginationViewModel.PerPage <= 1 && _adminAdminsViewModel.PaginationViewModel.Page > 0)
                {
                    _adminAdminsViewModel.UpdatePage(_adminAdminsViewModel.PaginationViewModel.Page - 1);
                }
                else if (_adminAdminsViewModel.PaginationViewModel.TotalElements - _adminAdminsViewModel.PaginationViewModel.Page * _adminAdminsViewModel.PaginationViewModel.PerPage > 1)
                {
                    _adminAdminsViewModel.UpdatePage(_adminAdminsViewModel.PaginationViewModel.Page);
                }
                else
                {
                    _adminAdminsViewModel.UpdatePage(0);
                }
                _adminAdminsViewModel.Context.Notifier.ShowInformation($"Admin {_adminUsername} has been deleted successfully.");
            }
        }
    }
}
