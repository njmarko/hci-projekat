using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ICommentService
    {
        List<Comment> GetCommentsForTask(int taskId);

        Comment Create(Comment comment, int taskId, int senderId);
    }
}
