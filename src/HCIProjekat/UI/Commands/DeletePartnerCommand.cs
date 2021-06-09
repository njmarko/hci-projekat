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
    public class DeletePartnerCommand : ICommand
    {
        private readonly AdminPartnersViewModel _adminPartnersVm;
        private readonly int _partnerId;
        private readonly string _partnerName;
        private readonly IModalService _modalService;
        private readonly IPartnersService _partnerService;

        public event EventHandler CanExecuteChanged;

        public DeletePartnerCommand(AdminPartnersViewModel adminPartnersVm, int partnerId, string partnerName, IModalService modalService, IPartnersService partnerService)
        {
            _adminPartnersVm = adminPartnersVm;
            _partnerId = partnerId;
            _partnerName = partnerName;
            _modalService = modalService;
            _partnerService = partnerService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_modalService.ShowConfirmationDialog($"Are you sure you want to delete partner {_partnerName}?"))
            {
                // undo redo
                var partner = _partnerService.Get(_partnerId);
                _adminPartnersVm.AddItem(partner);
                _partnerService.Delete(_partnerId);
                if (_adminPartnersVm.PaginationViewModel.TotalElements - _adminPartnersVm.PaginationViewModel.Page * _adminPartnersVm.PaginationViewModel.PerPage <= 1 && _adminPartnersVm.PaginationViewModel.Page > 0)
                {
                    Console.WriteLine("Vrati");
                    _adminPartnersVm.UpdatePage(_adminPartnersVm.PaginationViewModel.Page - 1);
                } else if (_adminPartnersVm.PaginationViewModel.TotalElements - _adminPartnersVm.PaginationViewModel.Page * _adminPartnersVm.PaginationViewModel.PerPage > 1)
                {
                    Console.WriteLine("brisi");
                    _adminPartnersVm.UpdatePage(_adminPartnersVm.PaginationViewModel.Page);
                }
                else
                {
                    _adminPartnersVm.UpdatePage(0);
                }
                
                _adminPartnersVm.Context.Notifier.ShowInformation($"Partner {_partnerName} has been deleted successfully.");
            }
        }
    }
}
