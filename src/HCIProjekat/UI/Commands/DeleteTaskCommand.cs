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
    public class DeleteTaskCommand : ICommand
    {
        private readonly EventPlannerHomeViewModel _eventPlannerHomeVm;
        private readonly int _taskId;
        private readonly ITaskService _taskService;
        private readonly IModalService _modalService;

        public event EventHandler CanExecuteChanged;

        public DeleteTaskCommand(EventPlannerHomeViewModel eventPlannerHomeVm, int taskId, ITaskService taskService, IModalService modalService)
        {
            _eventPlannerHomeVm = eventPlannerHomeVm;
            _taskId = taskId;
            _taskService = taskService;
            _modalService = modalService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var task = _taskService.Get(_taskId);
            if (_modalService.ShowConfirmationDialog($"Are you sure you wan to delete task {task.Name}?"))
            {
                _eventPlannerHomeVm.AddItem(task);
                _taskService.Delete(_taskId);
                _eventPlannerHomeVm.FetchTasksForSelectedRequest();
                _eventPlannerHomeVm.Context.Notifier.ShowInformation($"Task {_taskId} has been deleted successfully.");
            }
        }
    }
}
