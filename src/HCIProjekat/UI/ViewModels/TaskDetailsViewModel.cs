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

namespace UI.ViewModels
{
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


        public bool CanComment => IsValid();
        public ICommand AddCommentCommand { get; set; }



        public ObservableCollection<ClientTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<ClientTaskOfferCardModel>();
        public ObservableCollection<CommentViewModel> CommentModels { get; private set; } = new ObservableCollection<CommentViewModel>();

        public ErrorMessageViewModel CommentError { get; private set; } = new ErrorMessageViewModel();

        public TaskDetailsViewModel(IApplicationContext context, ITaskService taskService, IOfferService offerService, ICommentService commentService) : base(context)
        {
            _taskService = taskService;
            _offerService = offerService;
            _commentService = commentService;
            AddCommentCommand = new AddCommentCommand(this, commentService);
            //_task = _taskService.GetTask(1);

            //ovo je samo za testiranje
            //Context.Store.CurrentUser = new Client { FirstName = "Dejan", LastName = "Djordjevic", Username = "dejandjordjevic", Password = "test123", DateOfBirth = DateTime.Now, Id = 1 };
            //Context.Store.CurrentUser = new Admin { FirstName = "Vidoje", LastName = "Gavrilovic", DateOfBirth = DateTime.Now, Username = "vidojegavrilovic", Password = "test123", Id=10 };

            //LoadComments();
            //UpdatePage(0);
        }


        private void LoadTask()
        {
            Task = _taskService.GetTask(TaskId);
            RequestDetailsRoute = $"RequestDetails?requestId={Task.Request.Id}";
            LoadComments();
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            TaskOfferModels.Clear();
            var page = _offerService.GetOffersForTask(_task.Id, new OffersForTaskPageRequest { Size = Size, Page = pageNumber});
            foreach (var entity in page.Entities)
            {
                

                TaskOfferModels.Add(new ClientTaskOfferCardModel 
                { 
                    PartnerName = entity.Offer.Partner.Name, 
                    Description = entity.Offer.Description,
                    OfferName = entity.Offer.Name,
                    //Image = entity.Offer.Image,
                    OfferPrice = entity.Offer.Price
                });
            }
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
                    Margin = margin
                });
            }
        }
    }
}
