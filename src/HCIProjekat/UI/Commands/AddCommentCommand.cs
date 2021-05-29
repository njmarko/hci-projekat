using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Context;
using UI.ViewModels;

namespace UI.Commands
{
    public class AddCommentCommand : ICommand
    {
        private readonly TaskDetailsViewModel _taskDetailsVm;
        private readonly ICommentService _commentService;


        public event EventHandler CanExecuteChanged;
        public AddCommentCommand(TaskDetailsViewModel taskDetailsViewModel, ICommentService commentService)
        {
            _taskDetailsVm = taskDetailsViewModel;
            _commentService = commentService;
            _taskDetailsVm.PropertyChanged += _taskDetails_PropertyChanged;

        }
        private void _taskDetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaskDetailsViewModel.CanComment))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _taskDetailsVm.CanComment;
        }

        public void Execute(object parameter)
        {
            
            var comment = new Comment 
            { 
                Content = _taskDetailsVm.CommentContent,
                SentDate = DateTime.Now,
            };

            _commentService.Create(comment, _taskDetailsVm.Task.Id, _taskDetailsVm.Context.Store.CurrentUser.Id);
            _taskDetailsVm.LoadComments();
            _taskDetailsVm.CommentContent = "";
            _taskDetailsVm.CommentAdded = true;

        }
    }
}
