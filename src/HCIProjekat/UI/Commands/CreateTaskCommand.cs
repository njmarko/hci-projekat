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
using ToastNotifications.Messages;
using UI.ViewModels;

namespace UI.Commands
{
    public class CreateTaskCommand : ICommand
    {
        private readonly EventPlannerHomeViewModel _vm;
        private readonly CreateTaskViewModel _createTaskVm;
        private readonly ITaskService _taskService;
        private readonly int _taskId;

        public event EventHandler CanExecuteChanged;

        public CreateTaskCommand(CreateTaskViewModel createTaskVm, EventPlannerHomeViewModel vm, ITaskService taskService, int taskId)
        {
            _vm = vm;
            _createTaskVm = createTaskVm;
            _taskService = taskService;
            _taskId = taskId;

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
                if (_taskId == -1)
                {
                    Create();
                }
                else
                {
                    Update();
                    (parameter as Window).DialogResult = true;
                    (parameter as Window).Close();
                }
            }
            catch (DuplicateTaskException exception)
            {
                _createTaskVm.TaskTypeError.ErrorMessage = exception.Message;
            }
        }

        private void Create()
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
            task = _taskService.Create(task, _createTaskVm.Request.Id);
            task.Active = false;
            _vm.AddItem(task);
            _vm.ToDo.Add(_vm.Map(task));
            _createTaskVm.ResetFields();
            _vm.ToDoAdded = true;
            _vm.Context.Notifier.ShowInformation("New task has been added successfully.");
            _vm.ToDoAdded = false;
        }

        private void Update()
        {
            var task = _taskService.Get(_taskId);
            _vm.AddItem(task);
            task.Name = _createTaskVm.Name;
            task.Description = _createTaskVm.Description;
            task.TaskType = _createTaskVm.TaskTypeValue.Type.Value;
            _taskService.Update(task);
            _vm.Context.Notifier.ShowInformation("Task has been updated successfully.");
            _vm.FetchTasksForSelectedRequest();
        }
    }
}
