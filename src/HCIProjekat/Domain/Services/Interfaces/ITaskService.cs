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
        Task Update(Task task);
        void Delete(int taskId);
        List<Task> GetTasksForRequest(int requestId, string searchQuery);

        void RejectAllTaskOffers(int taskId);

        void RejectTaskOffer(int taskId, int taskOfferId);

        void AcceptTaskOffer(int taskId, int taskOfferId);
    }
}
