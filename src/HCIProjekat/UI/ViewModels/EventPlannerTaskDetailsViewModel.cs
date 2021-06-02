using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using UI.Context;
using UI.Util;

namespace UI.ViewModels
{
    public class EventPlannerTaskOfferCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
    }

    public class EventPlannerTaskDetailsViewModel : ViewModelBase
    {
        private readonly ITaskService _taskService;

        public EventPlannerTaskOffersViewModel AddedOffersVm { get; set; }
        public EventPlannerAvailableOffersViewModel AvailableOffersVm { get; set; }

        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value; FetchTask(); }
        }

        private Task _task;
        public Task Task
        {
            get { return _task; }
            set { _task = value; OnPropertyChanged(nameof(Task)); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged(nameof(Color)); }
        }

        public static string EventPlannerHome
        {
            get { return "EventPlannerHome"; }
        }

        public EventPlannerTaskDetailsViewModel(IApplicationContext context, ITaskService taskService, ITaskOfferService taskOfferService) : base(context)
        {
            _taskService = taskService;

            AddedOffersVm = new EventPlannerTaskOffersViewModel(context, taskOfferService);
            AvailableOffersVm = new EventPlannerAvailableOffersViewModel(context, taskOfferService);
        }

        private void FetchTask()
        {
            AddedOffersVm.TaskId = _taskId;
            Task = _taskService.GetTask(TaskId);
            TaskDetails taskDetails = TaskColorAndStatus.GetTaskColorAndStatus(Task);
            Color = taskDetails.Color;
            Status = taskDetails.Status;
        }
    }
}
