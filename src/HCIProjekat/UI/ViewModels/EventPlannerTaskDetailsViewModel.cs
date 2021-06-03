using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UI.Commands;
using UI.Context;
using UI.Services.Interfaces;
using UI.Util;

namespace UI.ViewModels
{
    public class EventPlannerTaskOfferCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public string ButtonContent { get; set; }
        public ICommand ButtonAction { get; set; }
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

        public EventPlannerTaskDetailsViewModel(IApplicationContext context, ITaskService taskService, ITaskOfferService taskOfferService, IOfferService offerService, IModalService modalService) : base(context)
        {
            _taskService = taskService;

            AddedOffersVm = new EventPlannerTaskOffersViewModel(context, taskOfferService, modalService);
            AvailableOffersVm = new EventPlannerAvailableOffersViewModel(context, offerService, taskOfferService, modalService);

            AddedOffersVm.AvailableVm = AvailableOffersVm;
            AvailableOffersVm.AddedVm = AddedOffersVm;
        }

        private void FetchTask()
        {
            AddedOffersVm.TaskId = _taskId;
            AvailableOffersVm.TaskId = _taskId;

            Task = _taskService.GetTask(TaskId);
            TaskDetails taskDetails = TaskColorAndStatus.GetTaskColorAndStatus(Task);
            Color = taskDetails.Color;
            Status = taskDetails.Status;
        }
    }
}
