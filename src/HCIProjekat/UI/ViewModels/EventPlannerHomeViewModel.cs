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
    public class TaskCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public bool CanToDo { get; set; }
        public bool CanInProgress { get; set; }
        public bool CanSentToClient { get; set; }
    }

    public class EventPlannerHomeViewModel : ViewModelBase
    {
        private readonly IEventPlannersService _eventPlannersService;
        private readonly ITaskService _taskService;
        private readonly IModalService _modalService;
        public ObservableCollection<Request> ActiveRequests { get; private set; }
        public ObservableCollection<TaskCardModel> ToDo { get; private set; } = new ObservableCollection<TaskCardModel>(); 
        public ObservableCollection<TaskCardModel> InProgress { get; private set; } = new ObservableCollection<TaskCardModel>(); 
        public ObservableCollection<TaskCardModel> SentToClient { get; private set; } = new ObservableCollection<TaskCardModel>(); 
        public ObservableCollection<TaskCardModel> Accepted { get; private set; } = new ObservableCollection<TaskCardModel>(); 
        public ObservableCollection<TaskCardModel> Rejected { get; private set; } = new ObservableCollection<TaskCardModel>(); 
        
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
            ActiveRequests = new ObservableCollection<Request>(_eventPlannersService.GetActiveRequests(Context.Store.CurrentUser.Id));
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
                FetchTasksForSelectedRequest();
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
            var taskModel = new TaskCardModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                TaskType = task.TaskType.ToString().ToUpper()[0] + task.TaskType.ToString().ToLower()[1..],
                CanToDo = task.TaskStatus == TaskStatus.IN_PROGRESS || task.TaskStatus == TaskStatus.REJECTED,
                CanInProgress = task.TaskStatus == TaskStatus.TO_DO || task.TaskStatus == TaskStatus.REJECTED,
                CanSentToClient = task.TaskStatus == TaskStatus.IN_PROGRESS
            };
            switch (task.TaskStatus)
            {
                case TaskStatus.TO_DO:
                    ToDo.Add(taskModel);
                    break;
                case TaskStatus.IN_PROGRESS:
                    InProgress.Add(taskModel);
                    break;
                case TaskStatus.SENT_TO_CLIENT:
                    SentToClient.Add(taskModel);
                    break;
                case TaskStatus.ACCEPTED:
                    Accepted.Add(taskModel);
                    break;
                case TaskStatus.REJECTED:
                    Rejected.Add(taskModel);
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
