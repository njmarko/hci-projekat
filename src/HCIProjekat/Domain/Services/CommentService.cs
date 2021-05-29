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

        public CommentService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
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
            //throw new NotImplementedException();
            using var context = _dbContextFactory.CreateDbContext();
            RequestParticipant sender = (RequestParticipant)context.Users.Find(senderId);
            comment.Sender = sender;
            comment.Task = context.Tasks.Find(taskId);
            context.Comments.Add(comment);
            context.SaveChanges();
            return comment;
        }

        public List<Comment> GetCommentsForTask(int taskId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Comments
                          .Include(c => c.Sender)
                          .Where(c => c.Task.Id == taskId)
                          .OrderBy(c => c.SentDate)
                          .ToList();
        }
    }
}
