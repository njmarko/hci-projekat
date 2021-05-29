﻿using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public TaskService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<Task> GetTasksForRequest(int requestId, string searchQuery)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var query = searchQuery.ToLower();
            return context.Tasks
                          .Where(t => t.Request.Id == requestId)
                          .Where(t => t.Name.ToLower().Contains(searchQuery)
                          || t.Description.ToLower().Contains(searchQuery)
                          || t.TaskType.ToString().ToLower().Contains(searchQuery))
                          .ToList();
        }
    }
}
