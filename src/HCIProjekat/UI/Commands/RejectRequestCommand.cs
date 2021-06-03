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
    public class RejectRequestCommand : ICommand
    {
        private readonly EventPlannerRequestsViewModel _eventPlannerRequestsVm;
        private readonly IRequestService _requestService;
        private readonly IModalService _modalService;
        private readonly int _requestId;

        public event EventHandler CanExecuteChanged;

        public RejectRequestCommand(EventPlannerRequestsViewModel eventPlannerRequestsVm, IRequestService requestService, IModalService modalService, int requestId)
        {
            _eventPlannerRequestsVm = eventPlannerRequestsVm;
            _requestService = requestService;
            _modalService = modalService;
            _requestId = requestId;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_modalService.ShowConfirmationDialog($"Are you sure you want to reject request {_requestId}?"))
            {
                var request = _requestService.Get(_requestId);
                _eventPlannerRequestsVm.AddItem(request);
                _requestService.Reject(_requestId, _eventPlannerRequestsVm.Context.Store.CurrentUser.Id);
                _eventPlannerRequestsVm.Context.Notifier.ShowInformation($"Request {_requestId} has been rejected.");
                _eventPlannerRequestsVm.UpdatePage(0);
            }
        }
    }
}
