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
        private readonly ITaskService _taskService;
        private readonly ClientTaskOfferCardModel _clientTaskOfferCardModel;
        private readonly TaskDetailsViewModel _taskDetailsViewModel;

        public AcceptTaskOfferCommand(ITaskService taskService, ClientTaskOfferCardModel clientTaskOfferCardModel, TaskDetailsViewModel taskDetailsViewModel)
        {
            _taskService = taskService;
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
            _taskService.AcceptTaskOffer(_clientTaskOfferCardModel.TaskId, _clientTaskOfferCardModel.TaskOfferId);
            _taskDetailsViewModel.TaskId = _clientTaskOfferCardModel.TaskId;
            _taskDetailsViewModel.UpdatePage(0);
        }
    }
}
