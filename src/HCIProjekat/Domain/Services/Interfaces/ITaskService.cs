using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ITaskService
    {
        List<Task> GetTasksForRequest(int requestId, string searchQuery);
    }
}
