using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class RejectTaskOfferCommand : ICommand
    {
        private readonly ITaskOfferService _taskOfferService;
        private readonly ClientTaskOfferCardModel _clientTaskOfferCardModel;
        private readonly TaskDetailsViewModel _taskDetailsViewModel;

        public RejectTaskOfferCommand(ITaskOfferService taskOfferService, ClientTaskOfferCardModel clientTaskOfferCardModel, TaskDetailsViewModel taskDetailsViewModel)
        {
            _taskOfferService = taskOfferService;
            _clientTaskOfferCardModel = clientTaskOfferCardModel;
            _taskDetailsViewModel = taskDetailsViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var offer = _taskOfferService.Get(_clientTaskOfferCardModel.TaskOfferId);
            _taskOfferService.RejectTaskOffer(_clientTaskOfferCardModel.TaskId, _clientTaskOfferCardModel.TaskOfferId);
           
            _taskDetailsViewModel.TaskId = _clientTaskOfferCardModel.TaskId;
            
            offer.OfferStatus = OfferStatus.PENDING;
            _taskDetailsViewModel.AddItem(offer);
            _taskDetailsViewModel.UpdatePage(0);
        }
    }
}
