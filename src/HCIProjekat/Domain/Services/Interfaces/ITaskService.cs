using Domain.Entities;
<<<<<<< HEAD
using Domain.Pagination;
using Domain.Pagination.Requests;
=======
>>>>>>> 451de900e0a34cad7a5d027f31e7cfda29b89d29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ITaskService
    {
<<<<<<< HEAD
        Page<Task> GetTasksForRequest(int requestId, TasksPageRequest pageRequest);

        Task GetTask(int taskId);
=======
        List<Task> GetTasksForRequest(int requestId, string searchQuery);
>>>>>>> 451de900e0a34cad7a5d027f31e7cfda29b89d29
    }
}
