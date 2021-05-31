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
        private readonly ITaskService _taskService;

        public RejectAllTaskOffersCommand(TaskDetailsViewModel taskDetailsViewModel, ITaskService taskService)
        {
            _taskDetailsViewModel = taskDetailsViewModel;
            _taskService = taskService;
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
            _taskService.RejectAllTaskOffers(taskId);
            _taskDetailsViewModel.UpdatePage(0);
            _taskDetailsViewModel.TaskId = taskId;
        }
    }
}
