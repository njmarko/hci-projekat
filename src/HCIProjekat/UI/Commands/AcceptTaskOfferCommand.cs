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
    public class AcceptTaskOfferCommand : ICommand
    {
        private readonly ITaskOfferService _taskOfferService;
        private readonly ClientTaskOfferCardModel _clientTaskOfferCardModel;
        private readonly TaskDetailsViewModel _taskDetailsViewModel;

        public AcceptTaskOfferCommand(ITaskOfferService taskOfferService, ClientTaskOfferCardModel clientTaskOfferCardModel, TaskDetailsViewModel taskDetailsViewModel)
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
            var offers = _taskOfferService.GetAllTaskOffersForTask(_clientTaskOfferCardModel.TaskId);
            //foreach (var offer in offers)
            //   _taskDetailsViewModel.AddItem(offer);

            _taskDetailsViewModel.AddItem(offers.Last());
            _taskOfferService.AcceptTaskOffer(_clientTaskOfferCardModel.TaskId, _clientTaskOfferCardModel.TaskOfferId);
            _taskDetailsViewModel.TaskId = _clientTaskOfferCardModel.TaskId;
            _taskDetailsViewModel.UpdatePage(_taskDetailsViewModel.PaginationViewModel.Page);
        }
    }
}
