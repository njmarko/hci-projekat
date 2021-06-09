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
    public class RejectAllTaskOffersCommand : ICommand
    {
        private readonly TaskDetailsViewModel _taskDetailsViewModel;
        private readonly ITaskOfferService _taskOfferService;

        public RejectAllTaskOffersCommand(TaskDetailsViewModel taskDetailsViewModel, ITaskOfferService taskOfferService)
        {
            _taskDetailsViewModel = taskDetailsViewModel;
            _taskOfferService = taskOfferService;
            _taskDetailsViewModel.PropertyChanged += _taskDetails_PropertyChanged;

        }

        public event EventHandler CanExecuteChanged;
        
        private void _taskDetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaskDetailsViewModel.CanRejectAllOffers))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }


        public bool CanExecute(object parameter)
        {
            return _taskDetailsViewModel.CanRejectAllOffers;
        }

        public void Execute(object parameter)
        {
            //throw new NotImplementedException();
            int taskId = _taskDetailsViewModel.Task.Id;
            var offers = _taskOfferService.GetAllTaskOffersForTask(taskId);
            if (offers.Count > 0)
            {
                _taskDetailsViewModel.AddItem(offers.Last());
            }
            _taskOfferService.RejectAllTaskOffers(taskId);
            _taskDetailsViewModel.UpdatePage(0);
            _taskDetailsViewModel.TaskId = taskId;
        }
    }
}
