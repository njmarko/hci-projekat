using Domain.Pagination;
﻿using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Pagination.Requests;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

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
            return context.Tasks
                .Where(t => t.Id == taskId)
                .Include(t => t.Request)
                .First();
        }

        public Page<Task> GetTasksForRequest(int requestId, TasksPageRequest page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Tasks
                          .Where(t => t.Request.Id == requestId)
                          .Where(t =>
                            t.Name.ToLower().Contains(page.Query.ToLower()) ||
                            t.Description.ToLower().Contains(page.Query.ToLower()))
                          .Where(t => page.Type == null || t.TaskType == page.Type)
                          .Where(t=> t.TaskStatus == TaskStatus.SENT_TO_CLIENT || 
                              t.TaskStatus == TaskStatus.ACCEPTED || 
                              t.TaskStatus == TaskStatus.REJECTED)
                          .Where(t=> page.Status == null || t.TaskStatus == page.Status)
                          .ToPage(page);
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
