using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Commands;
using UI.Context;
using UI.Services.Interfaces;

namespace UI.ViewModels
{
    public class EventPlannerCommentsViewModel : ViewModelBase
    {
        private readonly ICommentService _commentService;

        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set {  _taskId = value; OnPropertyChanged(nameof(TaskId)); LoadComments(); }
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

        public bool CanComment => !string.IsNullOrEmpty(CommentContent);

        public ICommand AddCommentCommand { get; set; }

        public ObservableCollection<CommentViewModel> CommentModels { get; private set; } = new ObservableCollection<CommentViewModel>();

        public EventPlannerCommentsViewModel(IApplicationContext context, ICommentService commentService, IModalService modalService) : base(context)
        {
            _commentService = commentService;

            AddCommentCommand = new EventPlannerCommentCommand(this, modalService, commentService);
            CommentAdded = false;
        }

        public void LoadComments()
        {
            CommentModels.Clear();
            CommentAdded = false;
            var comments = _commentService.GetCommentsForTask(_taskId);
            foreach (var comment in comments)
            {
                string color;
                string margin;
                var user = Context.Store.CurrentUser;

                if (user.Id == comment.Sender.Id)
                {
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
