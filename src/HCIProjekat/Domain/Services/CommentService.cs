using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CommentService : ICommentService
    {

        private readonly IApplicationDbContextFactory _dbContextFactory;

        public CommentService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<Comment> GetCommentsForTask(int taskId)
        {
            var context = _dbContextFactory.CreateDbContext();
            return context.Comments
                          .Include(c => c.Sender)
                          .Where(c => c.Task.Id == taskId)
                          .OrderBy(c => c.SentDate)
                          .ToList();
        }
    }
}
