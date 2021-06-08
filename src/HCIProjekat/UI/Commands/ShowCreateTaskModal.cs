using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Modals;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Commands
{
    public class ShowCreateTaskModal : ICommand
    {
        private readonly EventPlannerHomeViewModel _vm;
        private readonly ITaskService _taskService;
        private readonly IModalService _modalService;
        private readonly int _taskId;

        public event EventHandler CanExecuteChanged;

        public ShowCreateTaskModal(EventPlannerHomeViewModel vm, ITaskService taskService, IModalService modalService, int taskId)
        {
            _vm = vm;
            _taskService = taskService;
            _modalService = modalService;
            _taskId = taskId;

            _vm.PropertyChanged += _vm_PropertyChanged;
        }

        private void _vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EventPlannerHomeViewModel.CanAddTask))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _vm.CurrentRequest != null;
        }

        public void Execute(object parameter)
        {
            _modalService.ShowModal<CreateTaskModal>(new CreateTaskViewModel(_vm, _vm.Context, _taskService, _vm.CurrentRequest, _taskId));
        }
    }
}
