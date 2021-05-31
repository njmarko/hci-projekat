﻿using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class CreateTaskViewModel : ViewModelBase, ISelfValidatingViewModel
    {
        private readonly ITaskService _taskService;
        public Request Request { get; private set; }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanCreateTask)); }
        }

        public bool CanCreateTask => IsValid();

        private ServiceTypeModel _taskType;
        public ServiceTypeModel TaskTypeValue
        {
            get { return _taskType; }
            set { _taskType = value; OnPropertyChanged(nameof(TaskTypeValue)); }
        }

        public ObservableCollection<ServiceTypeModel> TaskTypeModels { get; private set; } = new ObservableCollection<ServiceTypeModel>();
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();

        public CreateTaskViewModel(IApplicationContext context, ITaskService taskService, Request request) : base(context)
        {
            _taskService = taskService;
            Request = request;

            TaskTypeValue = new ServiceTypeModel { Name = "Location", Type = ServiceType.LOCATION };
            TaskTypeModels.Add(TaskTypeValue);
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Catering", Type = ServiceType.CATERING });
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Music", Type = ServiceType.MUSIC });
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Photography", Type = ServiceType.PHOTOGRAPHY });
            TaskTypeModels.Add(new ServiceTypeModel { Name = "Animator", Type = ServiceType.ANIMATOR });
        }

        public bool IsValid()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(Name))
            {
                NameError.ErrorMessage = "Task name is required.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            return valid;
        }
    }
}
