using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class CreateTaskCommand : ICommand
    {
        private readonly CreateTaskViewModel _createTaskVm;
        private readonly ITaskService _taskService;

        public event EventHandler CanExecuteChanged;

        public CreateTaskCommand(CreateTaskViewModel createTaskVm, ITaskService taskService)
        {
            _createTaskVm = createTaskVm;
            _taskService = taskService;

            _createTaskVm.PropertyChanged += _createTaskVm_PropertyChanged;
        }

        private void _createTaskVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateTaskViewModel.CanCreateTask))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _createTaskVm.CanCreateTask;
        }

        public void Execute(object parameter)
        {
            try
            {
                var task = new Task
                {
                    Name = _createTaskVm.Name,
                    Comments = new List<Comment>(),
                    Offers = new List<TaskOffer>(),
                    Description = _createTaskVm.Description,
                    TaskStatus = TaskStatus.TO_DO,
                    TaskType = _createTaskVm.TaskTypeValue.Type.Value
                };
                _taskService.Create(task, _createTaskVm.Request.Id);
                (parameter as Window).DialogResult = true;
                (parameter as Window).Close();
            }
            catch (DuplicateTaskException exception)
            {
                _createTaskVm.TaskTypeError.ErrorMessage = exception.Message;
            }
        }
    }
}
