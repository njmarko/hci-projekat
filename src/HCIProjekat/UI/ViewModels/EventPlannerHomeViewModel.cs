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
        public bool CanEdit { get; set; }
        public bool CanDelete => CanToDo || CanInProgress || CanSentToClient;
        public string Route { get; set; }
        public EventPlannerHomeViewModel Vm { get; set; }
        public IApplicationContext Context { get; set; }

        public ICommand MoveToDo { get; private set; }
        public ICommand MoveInProgress { get; private set; }
        public ICommand MoveSentToClient { get; private set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }

        public TaskCardModel()
        {
            MoveToDo = new DelegateCommand(() => Vm.UpdateTaskStatus(Id, TaskStatus.TO_DO));
            MoveInProgress = new DelegateCommand(() => Vm.UpdateTaskStatus(Id, TaskStatus.IN_PROGRESS));
            MoveSentToClient = new DelegateCommand(() => Vm.UpdateTaskStatus(Id, TaskStatus.SENT_TO_CLIENT));
        }
    }

    public class EventPlannerHomeViewModel : UndoModelBase<Task>
    {
        private readonly IEventPlannersService _eventPlannersService;
        private readonly ITaskService _taskService;
        private readonly IModalService _modalService;
        private readonly IDictionary<TaskStatus, ObservableCollection<TaskCardModel>> _taskCollections;
        private readonly IDictionary<TaskStatus, string> _taskStatusMessages;
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
                    OnPropertyChanged(nameof(CanAddTask));
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

        private bool _allowToDoDrop;
        public bool AllowToDoDrop
        {
            get { return _allowToDoDrop; }
            set { _allowToDoDrop = value; OnPropertyChanged(nameof(AllowToDoDrop)); }
        }

        private bool _allowInProgressDrop;
        public bool AllowInProgressDrop
        {
            get { return _allowInProgressDrop; }
            set { _allowInProgressDrop = value; OnPropertyChanged(nameof(AllowInProgressDrop)); }
        }

        private bool _toDoAdded = false;
        public bool ToDoAdded
        {
            get { return _toDoAdded; }
            set { _toDoAdded = value; OnPropertyChanged(nameof(ToDoAdded)); }
        }
        private bool _inProgressAdded = false;
        public bool InProgressAdded
        {
            get { return _inProgressAdded; }
            set { _inProgressAdded = value; OnPropertyChanged(nameof(InProgressAdded)); }
        }
        private bool _sentToClientAdded = false;
        public bool SentToClientAdded
        {
            get { return _sentToClientAdded; }
            set { _sentToClientAdded = value; OnPropertyChanged(nameof(SentToClientAdded)); }
        }

        public bool CanAddTask => CurrentRequest != null;

        public ICommand Search { get; private set; }
        public ICommand ShowCreateTaskModal { get; private set; }

        public EventPlannerHomeViewModel(IApplicationContext context, IEventPlannersService eventPlannersService, ITaskService taskService, IModalService modalService) : base(context, taskService, modalService)
        {
            _eventPlannersService = eventPlannersService;
            _taskService = taskService;
            _modalService = modalService;
            Search = new DelegateCommand(FetchTasksForSelectedRequest);
            ShowCreateTaskModal = new ShowCreateTaskModal(this, _taskService, _modalService, -1);
            ActiveRequests = new ObservableCollection<Request>(_eventPlannersService.GetActiveRequests(Context.Store.CurrentUser.Id));
            HelpPage = "event-planner-home-page";
            if (ActiveRequests.Count > 0)
            {
                CurrentRequest = ActiveRequests[0];
            }

            _taskCollections = new Dictionary<TaskStatus, ObservableCollection<TaskCardModel>>()
            {
                [TaskStatus.TO_DO] = ToDo,
                [TaskStatus.IN_PROGRESS] = InProgress,
                [TaskStatus.SENT_TO_CLIENT] = SentToClient,
                [TaskStatus.ACCEPTED] = Accepted,
                [TaskStatus.REJECTED] = Rejected,
            };

            _taskStatusMessages = new Dictionary<TaskStatus, string>()
            {
                [TaskStatus.TO_DO] = "To do",
                [TaskStatus.IN_PROGRESS] = "In progress",
                [TaskStatus.SENT_TO_CLIENT] = "Sent to client",
                [TaskStatus.ACCEPTED] = "Accepted",
                [TaskStatus.REJECTED] = "Rejected",
            };

            AllowToDoDrop = false;
            AllowInProgressDrop = false;
        }

        public void UpdateTaskStatus(int taskId, TaskStatus taskStatus)
        {
            var task = _taskService.Get(taskId);
            if ((taskStatus == TaskStatus.SENT_TO_CLIENT) && !_modalService.ShowConfirmationDialog($"Are you sure you want to send task {task.Name} to the client for a review?"))
            {
                return;
            }
            AddItem(task);
            var fromStatus = task.TaskStatus;
            task.TaskStatus = taskStatus;
            _taskService.Update(task);
            MoveTask(taskId, fromStatus, taskStatus);
            SetScrollerEnd(taskStatus, true);
            Context.Notifier.ShowInformation($"Task {task.Name}'s status has been updated to {_taskStatusMessages[taskStatus]}.");
            SetScrollerEnd(taskStatus, false);
        }

        private void SetScrollerEnd(TaskStatus taskStatus, bool value)
        {
            if (taskStatus == TaskStatus.TO_DO)
            {
                ToDoAdded = value;
            }
            if (taskStatus == TaskStatus.IN_PROGRESS)
            {
                InProgressAdded = value;
            }
            if (taskStatus == TaskStatus.SENT_TO_CLIENT)
            {
                SentToClientAdded = value;
            }
        }

        public void FetchTasksForSelectedRequest()
        {
            if (_currentRequest != null)
            {
                ClearCollections();
                ToDoAdded = false;
                InProgressAdded = false;
                SentToClientAdded = false;
                var tasks = _taskService.GetTasksForRequest(_currentRequest.Id, SearchQuery);
                foreach (var task in tasks)
                {
                    InsertTask(task);
                }
            }
        }

        private void MoveTask(int tasksId, TaskStatus fromStatus, TaskStatus toStatus)
        {
            var taskModel = _taskCollections[fromStatus].FirstOrDefault(t => t.Id == tasksId);
            _taskCollections[fromStatus].Remove(taskModel);
            _taskCollections[toStatus].Add(Map(_taskService.Get(tasksId)));
        }

        private void InsertTask(Task task)
        {
            var taskModel = Map(task);
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

        public TaskCardModel Map(Task task)
        {
            var taskModel = new TaskCardModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                TaskType = task.TaskType.ToString().ToUpper()[0] + task.TaskType.ToString().ToLower()[1..],
                CanToDo = task.TaskStatus == TaskStatus.IN_PROGRESS || task.TaskStatus == TaskStatus.REJECTED,
                CanInProgress = task.TaskStatus == TaskStatus.TO_DO || task.TaskStatus == TaskStatus.REJECTED,
                CanSentToClient = task.TaskStatus == TaskStatus.IN_PROGRESS,
                CanEdit = task.TaskStatus == TaskStatus.TO_DO || task.TaskStatus == TaskStatus.IN_PROGRESS,
                Route = $"EventPlannerTaskDetails?taskId={task.Id}",
                Context = Context,
                Vm = this
            };
            taskModel.Edit = new ShowCreateTaskModal(this, _taskService, _modalService, task.Id);
            taskModel.Delete = new DeleteTaskCommand(this, task.Id, _taskService, _modalService);
            return taskModel;
        }

        private void ClearCollections()
        {
            ToDo.Clear();
            InProgress.Clear();
            SentToClient.Clear();
            Accepted.Clear();
            Rejected.Clear();
        }

        public override void UpdatePage(int pageNumber)
        {
            FetchTasksForSelectedRequest();
        }
    }
}
