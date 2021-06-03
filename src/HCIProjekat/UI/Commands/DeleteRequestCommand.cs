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
    public class DeleteRequestCommand : ICommand
    {
        private readonly ClientRequestsViewModel _clientRequestsVm;
        private readonly int _requestId;
        private readonly IModalService _modalService;
        private readonly IRequestService _requestService;

        public event EventHandler CanExecuteChanged;

        public DeleteRequestCommand(ClientRequestsViewModel clientRequestsVm, int requestId, IModalService modalService, IRequestService requestService)
        {
            _clientRequestsVm = clientRequestsVm;
            _requestId = requestId;
            _modalService = modalService;
            _requestService = requestService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_modalService.ShowConfirmationDialog($"Are you sure you want to delete request {_requestId}?"))
            {
                var request = _requestService.Get(_requestId);
                _clientRequestsVm.AddItem(request);
                _requestService.Delete(_requestId);
                _clientRequestsVm.UpdatePage(0);
                _clientRequestsVm.Context.Notifier.ShowInformation($"Request {_requestId} has been deleted successfully.");
            }
        }
    }
}
