using Domain.Pagination;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Persistence;
using Domain.Pagination.Requests;

namespace Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public TaskService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task GetTask(int taskId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Tasks.First(t => t.Id == taskId);
        }

        public Page<Task> GetTasksForRequest(int requestId, TasksPageRequest page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            
            return context.Tasks
                .Where(t=>t.Request.Id == requestId)
                .Where(t=>t.Name.ToLower().Contains(page.TaskName.ToLower()))
                .ToPage(page);
           
            
                      

        }
    }
}
