using Domain.Entities;
using Domain.Pagination;
using Domain.Pagination.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ITaskService
    {
        Page<Task> GetTasksForRequest(int requestId, TasksPageRequest pageRequest);
        Task Create(Task task, int requestId);
        Task GetTask(int taskId);
        List<Task> GetTasksForRequest(int requestId, string searchQuery);
    }
}
