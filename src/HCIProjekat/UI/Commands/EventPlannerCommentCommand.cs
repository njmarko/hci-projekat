using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Commands
{
    public class EventPlannerCommentCommand : ICommand
    {
        private readonly EventPlannerCommentsViewModel _plannerVm;
        private readonly IModalService _modalService;
        private readonly ICommentService _commentService;

        public event EventHandler CanExecuteChanged;

        public EventPlannerCommentCommand(EventPlannerCommentsViewModel plannerVm, IModalService modalService, ICommentService commentService)
        {
            _plannerVm = plannerVm;
            _modalService = modalService;
            _commentService = commentService;

            _plannerVm.PropertyChanged += _comment_PropertyChanged;
        }

        private void _comment_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EventPlannerCommentsViewModel.CanComment))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _plannerVm.CanComment;
        }

        public void Execute(object parameter)
        {
                var comment = new Comment
                {
                    Content = _plannerVm.CommentContent,
                    SentDate = DateTime.Now,
                };

                _commentService.Create(comment, _plannerVm.TaskId, _plannerVm.Context.Store.CurrentUser.Id);
                _plannerVm.LoadComments();
                _plannerVm.CommentContent = "";
                _plannerVm.CommentAdded = true;
        }
    }
}
