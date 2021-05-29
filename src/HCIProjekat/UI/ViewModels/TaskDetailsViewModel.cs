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

namespace UI.ViewModels
{
    public class TaskDetailsViewModel : PagingViewModelBase
    {
        private readonly ITaskService _taskService;
        private readonly IOfferService _offerService;
        private readonly ICommentService _commentService;
        
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

        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set 
            { 
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }




        public ObservableCollection<ClientTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<ClientTaskOfferCardModel>();
        public ObservableCollection<CommentViewModel> CommentModels { get; private set; } = new ObservableCollection<CommentViewModel>();


        public TaskDetailsViewModel(IApplicationContext context, ITaskService taskService, IOfferService offerService, ICommentService commentService) : base(context)
        {
            _taskService = taskService;
            _offerService = offerService;
            _commentService = commentService;
            _task = _taskService.GetTask(1);
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

        private void LoadComments()
        {
            CommentModels.Clear();
            var comments = _commentService.GetCommentsForTask(_task.Id);
            foreach(var comment in comments)
            {
                string color;// = Context.Store.LoggedUserId == comment.Sender.Id ? "Blue" : "White";
                string margin;

                if (Context.Store.CurrentUser.Id == comment.Sender.Id) {
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
