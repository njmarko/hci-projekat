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

        public bool IsPending { get; set; }

        public int TaskOfferId { get; set; }

        public int TaskId { get; set; }

        public ICommand RejectTaskOffer { get; set; }

        public ICommand AcceptTaskOffer { get; set; }

    }
    public class TaskDetailsViewModel : PagingViewModelBase, ISelfValidatingViewModel
    {
        private readonly ITaskService _taskService;
        private readonly IOfferService _offerService;
        private readonly ICommentService _commentService;

        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set
            {
                _taskId = value;
                OnPropertyChanged(nameof(TaskId));
                LoadTask();
            }
        }


        private Task _task;

        public Task Task
        {
            get { return _task; }
            set
            {
                _task = value;
                OnPropertyChanged(nameof(IsPending));
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

        public bool IsPending => Task.TaskStatus == TaskStatus.SENT_TO_CLIENT;



        public bool CanComment => IsValid();

        public bool CanRejectAllOffers => AbleToReject();

        public ICommand AddCommentCommand { get; set; }

        public ICommand Reject { get; set; }



        public ObservableCollection<ClientTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<ClientTaskOfferCardModel>();
        public ObservableCollection<CommentViewModel> CommentModels { get; private set; } = new ObservableCollection<CommentViewModel>();

        public ErrorMessageViewModel CommentError { get; private set; } = new ErrorMessageViewModel();

        public TaskDetailsViewModel(IApplicationContext context, ITaskService taskService, IOfferService offerService, ICommentService commentService) : base(context)
        {
            _taskService = taskService;
            _offerService = offerService;
            _commentService = commentService;
            AddCommentCommand = new AddCommentCommand(this, commentService);
            Reject = new RejectAllTaskOffersCommand(this, taskService);
        }
        private bool AbleToReject()
        {
            return Task.TaskStatus == TaskStatus.SENT_TO_CLIENT;
        }
        

        private void LoadTask()
        {
            Task = _taskService.GetTask(TaskId);
            switch (Task.TaskStatus)
            {
                case TaskStatus.SENT_TO_CLIENT:
                    Color = "#fcc428";
                    Status = "Pending";
                    break;
                case TaskStatus.REJECTED:
                    Color = "#de1212";
                    Status = "Rejected";
                    break;
                case TaskStatus.ACCEPTED:
                    Color = "#088a35";
                    Status = "Accepted";
                    break;
            }

            RequestDetailsRoute = $"RequestDetails?requestId={Task.Request.Id}";
            LoadComments();
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            TaskOfferModels.Clear();
            var page = _offerService.GetOffersForTask(_task.Id, new OffersForTaskPageRequest { Size = Size, Page = pageNumber});
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
                    //Image = entity.Offer.Image,
                    OfferPrice = entity.Offer.Price,
                    Color = color,
                    Status = status,
                    IsPending = entity.OfferStatus == OfferStatus.PENDING,
                    TaskOfferId = entity.Id,
                    TaskId = Task.Id
                };

                taskOfferCardModel.RejectTaskOffer = new RejectTaskOfferCommand(_taskService, taskOfferCardModel, this);
                taskOfferCardModel.AcceptTaskOffer = new AcceptTaskOfferCommand(_taskService, taskOfferCardModel, this);

                TaskOfferModels.Add(taskOfferCardModel);
            }
            OnPropertyChanged(nameof(CanRejectAllOffers));
            OnPageFetched(page);

        }

        public bool IsValid()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(CommentContent))
            {
                valid = false;
                CommentError.ErrorMessage = "Comment content cannot be empty.";
            }
            else
            {
                valid = true;
                CommentError.ErrorMessage = null;

            }


            return valid;

        }

        public void LoadComments()
        {
            CommentModels.Clear();
            var comments = _commentService.GetCommentsForTask(_task.Id);
            foreach(var comment in comments)
            {
                string color;
                string margin;
                var user = Context.Store.CurrentUser;

                if (user.Id == comment.Sender.Id) {
                //ovo je za sad hardkodovano, kada se poveze sa loginom vrsice se provjera
               // if (1 == comment.Sender.Id) {
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
