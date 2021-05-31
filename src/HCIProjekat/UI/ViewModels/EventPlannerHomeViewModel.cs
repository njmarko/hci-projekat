using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Commands;
using UI.Context;
using UI.Modals;
using UI.Services.Interfaces;

namespace UI.ViewModels
{
    public class EventPlannerHomeViewModel : ViewModelBase
    {
        private readonly IEventPlannersService _eventPlannersService;
        private readonly ITaskService _taskService;
        private readonly IModalService _modalService;
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

        private string _searchQuery = "";
        public string SearchQuery
        {
            get { return _searchQuery; }
            set { _searchQuery = value; OnPropertyChanged(nameof(SearchQuery)); }
        }

        public ICommand Search { get; private set; }
        public ICommand ShowCreateTaskModal { get; private set; }

        public EventPlannerHomeViewModel(IApplicationContext context, IEventPlannersService eventPlannersService, ITaskService taskService, IModalService modalService) : base(context)
        {
            _eventPlannersService = eventPlannersService;
            _taskService = taskService;
            _modalService = modalService;
            Search = new DelegateCommand(FetchTasksForSelectedRequest);
            ShowCreateTaskModal = new DelegateCommand(ShowCreateTask);
            ActiveRequests = new ObservableCollection<Request>(_eventPlannersService.GetActiveRequests(11)); // TODO: Zameni ovo sa Context.Store.CurrentUser.Id
            if (ActiveRequests.Count > 0)
            {
                CurrentRequest = ActiveRequests[0];
            }
        }

        private void ShowCreateTask()
        {
            var ok = _modalService.ShowModal<CreateTaskModal>(new CreateTaskViewModel(Context, _taskService, CurrentRequest));
            if (ok)
            {
                Context.Notifier.ShowInformation("New task has been created successfully.");
            }
        }

        private void FetchTasksForSelectedRequest()
        {
            if (_currentRequest != null)
            {
                ClearCollections();
                var tasks = _taskService.GetTasksForRequest(_currentRequest.Id, SearchQuery);
                foreach (var task in tasks)
                {
                    InsertTask(task);
                }
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
