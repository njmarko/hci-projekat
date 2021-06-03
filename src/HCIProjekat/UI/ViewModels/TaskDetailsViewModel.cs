using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UI.Context;
using UI.ViewModels.CardViewModels;
using Domain.Pagination.Requests;
using System.Windows;
using UI.Util;
using UI.ViewModels.Interfaces;
using System.Windows.Input;
using UI.Commands;
using Domain.Enums;
using System.Globalization;
using UI.Services.Interfaces;

namespace UI.ViewModels
{
    public class ClientTaskOfferCardModel
    {
        public string PartnerName { get; set; }
        public string OfferName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int OfferPrice { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }

        public int TaskOfferId { get; set; }

        public int TaskId { get; set; }

        public ICommand RejectTaskOffer { get; set; }

        public ICommand AcceptTaskOffer { get; set; }

        public bool IsVisible { get; set; }

    }
    public class TaskDetailsViewModel : UndoModelBase<TaskOffer>
    {
        private readonly ITaskService _taskService;
        private readonly ICommentService _commentService;
        private readonly ITaskOfferService _taskOfferService;

        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set
            {
                _taskId = value;
                OnPropertyChanged(nameof(TaskId));
                LoadTask(true);
            }
        }


        private Task _task;

        public Task Task
        {
            get { return _task; }
            set
            {
                _task = value;
                OnPropertyChanged(nameof(IsPendingAndIsClient));
                OnPropertyChanged(nameof(Task));
            }
        }

        private string _commentContent;

        public string CommentContent
        {
            get { return _commentContent; }
            set
            {
                _commentContent = value;
                OnPropertyChanged(nameof(CommentContent));
                OnPropertyChanged(nameof(CanComment));
            }
        }

        private bool _commentAdded;

        public bool CommentAdded
        {
            get { return _commentAdded; }
            set
            {
                _commentAdded = value;
                OnPropertyChanged(nameof(CommentAdded));
            }
        }

        private string _requestDetailsRoute;
        public string RequestDetailsRoute
        {
            get { return _requestDetailsRoute; }
            set
            {
                _requestDetailsRoute = value;
                OnPropertyChanged(nameof(RequestDetailsRoute));
            }
        }


        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public bool IsPendingAndIsClient => Task.TaskStatus == TaskStatus.SENT_TO_CLIENT && Context.Store.CurrentUser is Client;



        public bool CanComment => !string.IsNullOrEmpty(CommentContent);

        public bool CommentAreaVisible => Context.Store.CurrentUser is not Admin;

       

        public bool CanRejectAllOffers => AbleToReject();


        public ICommand AddCommentCommand { get; set; }

        public ICommand Reject { get; set; }

        public ICommand Search { get; private set; }



        public ObservableCollection<ClientTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<ClientTaskOfferCardModel>();
        public ObservableCollection<CommentViewModel> CommentModels { get; private set; } = new ObservableCollection<CommentViewModel>();


        public TaskDetailsViewModel(IApplicationContext context, ITaskService taskService, ITaskOfferService taskOfferService, ICommentService commentService, IModalService modalService) : base(context, taskOfferService, modalService)
        {
            Rows = 1;
            _taskService = taskService;
            _taskOfferService = taskOfferService;
            _commentService = commentService;
            AddCommentCommand = new AddCommentCommand(this, commentService, modalService);
            Reject = new RejectAllTaskOffersCommand(this, taskOfferService);
            CommentAdded = false;

            Search = new DelegateCommand(() => UpdatePage(0));
            SearchQuery = string.Empty;
        }
        private bool AbleToReject()
        {
            return Task.TaskStatus == TaskStatus.SENT_TO_CLIENT;
        }
        

        private void LoadTask(bool updatePage)
        {
            Task = _taskService.GetTask(TaskId);

            TaskDetails taskDetails = TaskColorAndStatus.GetTaskColorAndStatus(Task);
            Color = taskDetails.Color;
            Status = taskDetails.Status;

            RequestDetailsRoute = $"RequestDetails?requestId={Task.Request.Id}";
            LoadComments();
            if(updatePage)
                UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            LoadTask(false);
            TaskOfferModels.Clear();
            var page = _taskOfferService.GetOffersForTask(_task.Id, new OffersForTaskPageRequest { Size = Size, Page = pageNumber, SearchQuery=SearchQuery });
            string status = "";
            string color = "";
            
            foreach (var entity in page.Entities)
            {

                switch (entity.OfferStatus)
                {
                    case OfferStatus.PENDING:
                        color = "#fcc428";
                        status = "Pending";
                        break;
                    case OfferStatus.REJECTED:
                        color = "#de1212";
                        status = "Rejected";
                        break;
                    case OfferStatus.ACCEPTED:
                        color = "#088a35";
                        status = "Accepted";
                        break;

                }

                var taskOfferCardModel = new ClientTaskOfferCardModel
                {
                    PartnerName = entity.Offer.Partner.Name,
                    Description = entity.Offer.Description,
                    OfferName = entity.Offer.Name,
                    OfferPrice = entity.Offer.Price,
                    Color = color,
                    Status = status,
                    TaskOfferId = entity.Id,
                    TaskId = Task.Id,
                    IsVisible = entity.OfferStatus == OfferStatus.PENDING && Context.Store.CurrentUser is Client
                };

                taskOfferCardModel.RejectTaskOffer = new RejectTaskOfferCommand(_taskOfferService, taskOfferCardModel, this);
                taskOfferCardModel.AcceptTaskOffer = new AcceptTaskOfferCommand(_taskOfferService, taskOfferCardModel, this);

                TaskOfferModels.Add(taskOfferCardModel);
            }
            OnPropertyChanged(nameof(CanRejectAllOffers));
            OnPageFetched(page);

        }

        public void LoadComments()
        {
            CommentModels.Clear();
            CommentAdded = false;
            var comments = _commentService.GetCommentsForTask(_task.Id);
            foreach(var comment in comments)
            {
                string color;
                string margin;
                var user = Context.Store.CurrentUser;

                if (user.Id == comment.Sender.Id || (Context.Store.CurrentUser is Admin && comment.Sender is EventPlanner)) {
                    color = "#78cdff";
                    margin = "800,5,0,0";
                }
                
                else
                {
                    margin = "-800,5,0,0";
                    color = "White";
                }


                CommentModels.Add(new CommentViewModel
                {
                    SentDate = comment.SentDate,
                    Sender = comment.Sender.Username,
                    Content = comment.Content,
                    Color = color,
                    Margin = margin,
                });
            }
        }
    }
}
