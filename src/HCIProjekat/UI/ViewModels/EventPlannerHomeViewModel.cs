using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UI.Context;

namespace UI.ViewModels
{
    public class EventPlannerHomeViewModel : ViewModelBase
    {
        private readonly IEventPlannersService _eventPlannersService;
        private readonly ITaskService _taskService;

        public ObservableCollection<Request> ActiveRequests { get; private set; }
        public ObservableCollection<Task> ToDo { get; private set; } = new ObservableCollection<Task>(); 
        public ObservableCollection<Task> InProgress { get; private set; } = new ObservableCollection<Task>(); 
        public ObservableCollection<Task> SentToClient { get; private set; } = new ObservableCollection<Task>(); 
        public ObservableCollection<Task> Accepted { get; private set; } = new ObservableCollection<Task>(); 
        public ObservableCollection<Task> Rejected { get; private set; } = new ObservableCollection<Task>(); 
        
        private Request _currentRequest;
        public Request CurrentRequest
        {
            get { return _currentRequest; }
            set
            {
                if (_currentRequest != value)
                {
                    _currentRequest = value;
                    FetchTasksForSelectedRequest();
                }
            }
        }

        public EventPlannerHomeViewModel(IApplicationContext context, IEventPlannersService eventPlannersService, ITaskService taskService) : base(context)
        {
            _eventPlannersService = eventPlannersService;
            _taskService = taskService;
            ActiveRequests = new ObservableCollection<Request>(_eventPlannersService.GetActiveRequests(11));
            if (ActiveRequests.Count > 0)
            {
                CurrentRequest = ActiveRequests[0];
            }
        }

        private void FetchTasksForSelectedRequest()
        {
            ClearCollections();
            var tasks = _taskService.GetTasksForRequest(_currentRequest.Id, "");
            foreach (var task in tasks)
            {
                InsertTask(task);
            }
        }

        private void InsertTask(Task task)
        {
            switch (task.TaskStatus)
            {
                case TaskStatus.TO_DO:
                    ToDo.Add(task);
                    break;
                case TaskStatus.IN_PROGRESS:
                    InProgress.Add(task);
                    break;
                case TaskStatus.SENT_TO_CLIENT:
                    SentToClient.Add(task);
                    break;
                case TaskStatus.ACCEPTED:
                    Accepted.Add(task);
                    break;
                case TaskStatus.REJECTED:
                    Rejected.Add(task);
                    break;
            }
        }

        private void ClearCollections()
        {
            ToDo.Clear();
            InProgress.Clear();
            SentToClient.Clear();
            Accepted.Clear();
            Rejected.Clear();
        }
    }
}
