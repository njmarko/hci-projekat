﻿using Domain.Entities;
using Domain.Enums;
using Domain.Pagination.Requests;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.Modals;
using UI.Services.Interfaces;
using UI.ViewModels.CardViewModels;

namespace UI.ViewModels
{
    public class TaskTypeModel
    {
        public string Name { get; set; }
        public ServiceType? Type { get; set; }
    }

    public class TaskStatusModel 
    {
        public string Name { get; set; }

        public TaskStatus? TaskStatus { get; set; }
    }


    public class RequestDetailsViewModel : PagingViewModelBase
    {
        private readonly IRequestService _requestService;
        private readonly ITaskService _taskService;
        private readonly IModalService _modalService;
        private readonly TaskTypeModel _typeInitial;
        private readonly TaskStatusModel _statusInitial;

        private string _currentCost { get; set; }

        public string CurrentCost 
        {
            get { return _currentCost; }
            set 
            {
                _currentCost = value;
                OnPropertyChanged(nameof(CurrentCost));
            
            }
        }

        private Request _request;

        public Request Request
        {
            get { return _request; }
            set 
            { 
                _request = value;
                OnPropertyChanged(nameof(Request));
            }
        }

        private string _query;

        public string Query
        {
            get { return _query; }
            set 
            { 
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }
        private TaskTypeModel _taskType;
        public TaskTypeModel TaskTypeValue
        {
            get { return _taskType; }
            set { _taskType = value; OnPropertyChanged(nameof(TaskTypeValue)); }
        }

        private TaskStatusModel _taskStatus;
        public TaskStatusModel TaskStatusValue
        {
            get { return _taskStatus; }
            set 
            { 
                _taskStatus = value;
                OnPropertyChanged(nameof(TaskStatusValue));
            }
        }

        private int _requestId;
        public int RequestId
        {
            get { return _requestId; }
            set
            {
                _requestId = value;
                LoadRequest();
                OnPropertyChanged(nameof(RequestId));
            }
        }

        private bool _canEditSeatingLayout;
        public bool CanEditSeatingLayout
        {
            get { return _canEditSeatingLayout; }
            set { _canEditSeatingLayout = value; OnPropertyChanged(nameof(CanEditSeatingLayout)); }
        }

        private string _date;

        public string Date
        {
            get { return _date; }
            set 
            { 
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _budget;

        public string Budget
        {
            get 
            { 
                return _budget; 
            }
            
            set 
            { 
                _budget = value;
                OnPropertyChanged(nameof(Budget));
            }
        }



        public string TaskDetailsRoute { get; set; }
        public string BackRoute { get; private set; }

        public ObservableCollection<ClientTaskCardModel> TaskModels { get; private set; } = new ObservableCollection<ClientTaskCardModel>();
        public override ICommand Search { get; set; }
        public override ICommand Clear { get; set; }
        public ICommand ShowSeatingModal { get; set; }
        public ObservableCollection<TaskTypeModel> TaskTypeModels { get; private set; } = new ObservableCollection<TaskTypeModel>();
        public ObservableCollection<TaskStatusModel> TaskStatusModels { get; private set; } = new ObservableCollection<TaskStatusModel>();

        private void LoadRequest()
        {
            Request = _requestService.Get(RequestId);
            Date = Request.Date.ToString("dd.MM.yyyy.");
            Budget = $"{Request.Budget} RSD";
            CurrentCost = $"{_requestService.GetRequestCost(RequestId)} RSD";
            CanEditSeatingLayout = _requestService.IsLocationAccepted(RequestId);
            UpdatePage(0);
        }

        public RequestDetailsViewModel(IApplicationContext context, IRequestService requestService, ITaskService taskService, IModalService modalService) : base(context)
        {
            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);
            ShowSeatingModal = new DelegateCommand(ShowClientSeatingLayoutModal);
            Rows = 1;
            Columns = 4;
            Query = "";
            _requestService = requestService;
            _taskService = taskService;
            _modalService = modalService;
            
            _typeInitial = new TaskTypeModel { Name = "All types" };
            _taskType = _typeInitial;

            TaskTypeModels.Add(_typeInitial);
            TaskTypeModels.Add(new TaskTypeModel { Type = ServiceType.LOCATION, Name = "Location" });
            TaskTypeModels.Add(new TaskTypeModel { Type = ServiceType.CATERING, Name = "Catering" });
            TaskTypeModels.Add(new TaskTypeModel { Type = ServiceType.MUSIC, Name = "Music" });
            TaskTypeModels.Add(new TaskTypeModel { Type = ServiceType.PHOTOGRAPHY, Name = "Photography" });
            TaskTypeModels.Add(new TaskTypeModel { Type = ServiceType.ANIMATOR, Name = "Animator" });

            _statusInitial = new TaskStatusModel { Name = "All statuses" };
            _taskStatus = _statusInitial;

            TaskStatusModels.Add(_statusInitial);
            TaskStatusModels.Add(new TaskStatusModel { TaskStatus = TaskStatus.SENT_TO_CLIENT, Name = "Pending" });
            TaskStatusModels.Add(new TaskStatusModel { TaskStatus = TaskStatus.ACCEPTED, Name = "Accepted" });
            TaskStatusModels.Add(new TaskStatusModel { TaskStatus = TaskStatus.REJECTED, Name = "Rejected" });
            HelpPage = "client-request-details";

            if (Context.Store.CurrentUser is Client)
            {
                BackRoute = "ClientRequests";
                TaskDetailsRoute = "TaskDetails";
            }
            else if (Context.Store.CurrentUser is EventPlanner)
            {
                BackRoute = "EventPlannerRequests";
                TaskDetailsRoute = "EventPlannerTaskDetails";
            }
            else
            {
                BackRoute = "AdminRequests";
                TaskDetailsRoute = "TaskDetails";
            }
        }

        private void ShowClientSeatingLayoutModal()
        {
            _modalService.ShowModal<ClientSeatingLayoutModal>(new ClientSeatingLayoutViewModel(Context, _requestService, RequestId));
        }

        public void ClearFilters()
        {
            Query = string.Empty;
            TaskTypeValue = _typeInitial;
            TaskStatusValue = _statusInitial;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            TaskModels.Clear();
            var page = _taskService.GetTasksForRequest(_request.Id, new TasksPageRequest { Size = Size, Page = pageNumber, Query = Query, Type = TaskTypeValue.Type, Status = TaskStatusValue.TaskStatus});
            string color = "";
            string status = "";
            foreach (var entity in page.Entities)
            {
                switch (entity.TaskStatus)
                { 
                    case TaskStatus.SENT_TO_CLIENT:
                        color = "#fcc428";
                        status = "Pending";
                        break;
                    case TaskStatus.REJECTED:
                        color = "#de1212";
                        status = "Rejected";
                        break;
                    case TaskStatus.ACCEPTED:
                        color = "#088a35";
                        status = "Accepted";
                        break;

                }

                TaskModels.Add(new ClientTaskCardModel { 
                    Name = entity.Name,
                    Description = entity.Description, 
                    Type = entity.TaskType, 
                    Status = status, 
                    Color = color,
                    TaskStatus = entity.TaskStatus,
                    IsPending = entity.TaskStatus == TaskStatus.SENT_TO_CLIENT,
                    Context = Context,
                    Route = $"{TaskDetailsRoute}?taskId={entity.Id}"
                });
            }
            OnPageFetched(page);
        }
    }
}
