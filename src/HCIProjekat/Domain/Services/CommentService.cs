using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Domain.Services
{
    public class CommentService : ICommentService
    {

        private readonly IApplicationDbContextFactory _dbContextFactory;
        private readonly INotificationService _notificationService;

        public CommentService(IApplicationDbContextFactory dbContextFactory, INotificationService notificationService)
        {
            _dbContextFactory = dbContextFactory;
            _notificationService = notificationService;
        }

        public Comment Create(Comment comment)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Comments.Add(comment);
            context.SaveChanges();
            return comment;
        }

        public Comment Create(Comment comment, int taskId, int senderId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            RequestParticipant sender = (RequestParticipant) context.Users.Find(senderId);
            comment.Sender = sender;
            var task = context.Tasks
                                  .Include(t => t.Request)
                                  .ThenInclude(r => r.EventPlanner)
                                  .Include(t => t.Request)
                                  .ThenInclude(r => r.Client)
                                  .SingleOrDefault(t => t.Id == taskId);

            comment.Task = task;
            context.Comments.Add(comment);
            context.SaveChanges();

            // Notify the other request participant that the new comment has been left
            var recieverId = senderId == task.Request.Client.Id ? task.Request.EventPlanner.Id : task.Request.Client.Id;
            var notification = new Notification { Message = $"New comment left on task {task.Name}.", UserId = recieverId, RequestId = task.Request.Id, TaskId = task.Id };
            _notificationService.Push(notification);

            return comment;
        }

        public List<Comment> GetCommentsForTask(int taskId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Comments
                          .Include(c => c.Sender)
                          .Where(c => c.Active)
                          .Where(c => c.Task.Id == taskId)
                          .OrderBy(c => c.SentDate)
                          .ToList();
        }
    }
}
