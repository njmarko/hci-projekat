﻿using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.CustomAttributes;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class CreateTaskViewModel : ValidationModel<CreateTaskViewModel>
    {
        private readonly EventPlannerHomeViewModel _vm;
        private readonly ITaskService _taskService;
        public Request Request { get; private set; }

        private string _name = string.Empty;
        [ValidationField]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanCreateTask)); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public bool CanCreateTask => IsValid();

        private ServiceTypeModel _taskType;
        public ServiceTypeModel TaskTypeValue
        {
            get { return _taskType; }
            set { _taskType = value; OnPropertyChanged(nameof(TaskTypeValue)); }
        }

        public string HeadlineText { get; private set; } = "Create task";
        public string ButtonText { get; private set; } = "Create";

        public string Help { get; private set; } = "add-task";


        public ObservableCollection<ServiceTypeModel> TaskTypeModels { get; private set; } = new ObservableCollection<ServiceTypeModel>();
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel TaskTypeError { get; private set; } = new ErrorMessageViewModel();

        public ICommand CreateTask { get; private set; }

        public CreateTaskViewModel(EventPlannerHomeViewModel vm, IApplicationContext context, ITaskService taskService, Request request, int taskId = -1) : base(context)
        {
            _vm = vm;
            _taskService = taskService;
            Request = request;
            CreateTask = new CreateTaskCommand(this, _vm, _taskService, taskId);

            TaskTypeValue = new ServiceTypeModel { Name = "Location", Type = ServiceType.LOCATION };
            TaskTypeModels.Add(TaskTypeValue);
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Catering", Type = ServiceType.CATERING });
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Music", Type = ServiceType.MUSIC });
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Photography", Type = ServiceType.PHOTOGRAPHY });
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Animator", Type = ServiceType.ANIMATOR });

            if (taskId != -1)
            {
                var task = _taskService.Get(taskId);
                Name = task.Name;
                Description = task.Description;
                TaskTypeValue = TaskTypeModels.First(t => t.Type != null && t.Type == task.TaskType);

                HeadlineText = "Edit task";
                ButtonText = "Save";
                Help = "edit-task";
            } 
        }

        public void ResetFields()
        {
            Name = string.Empty;
            Description = string.Empty;
            TaskTypeValue = TaskTypeModels.First();
            ResetDirtyValues();
            IsValid();
        }

        public bool IsValid()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(Name) && IsDirty(nameof(Name)))
            {
                NameError.ErrorMessage = "Task name is required.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            TaskTypeError.ErrorMessage = null;
            return valid && AllDirty();
        }
    }
}
