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
using Domain.Exceptions;

namespace Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;
        private readonly INotificationService _notificationService;

        public TaskService(IApplicationDbContextFactory dbContextFactory, INotificationService notificationService)
        {
            _dbContextFactory = dbContextFactory;
            _notificationService = notificationService;
        }

        public Task Create(Task task, int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (task.TaskType == ServiceType.LOCATION)
            {
                var sameTask = context.Tasks
                                      .Where(t => t.Active)
                                      .Where(t => t.Request.Id == requestId)
                                      .Where(t => t.TaskType == ServiceType.LOCATION)
                                      .Where(t => t.TaskStatus != TaskStatus.REJECTED)
                                      .FirstOrDefault();
                if (sameTask != null)
                {
                    throw new DuplicateTaskException(ServiceType.LOCATION);
                }
            }

            task.Request = context.Requests.Find(requestId);

            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }

        public Task Get(int taskId)
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
                          .Where(t => t.Active)
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
                          .Where(t => t.Active)
                          .Where(t => t.Request.Id == requestId)
                          .Where(t => t.Name.ToLower().Contains(query)
                          || t.Description.ToLower().Contains(query)
                          || t.TaskType.ToString().ToLower().Contains(query))
                          .ToList();
        }

        public Task Update(Task task)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (task.TaskType == ServiceType.LOCATION)
            {
                var sameTask = context.Tasks
                                      .Where(t => t.Active)
                                      .Where(t => t.Id != task.Id)
                                      .Where(t => t.Request.Id == task.Request.Id)
                                      .Where(t => t.TaskType == ServiceType.LOCATION)
                                      .Where(t => t.TaskStatus != TaskStatus.REJECTED)
                                      .FirstOrDefault();
                if (sameTask != null)
                {
                    throw new DuplicateTaskException(ServiceType.LOCATION);
                }
            }

            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }

        public void Delete(int taskId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var task = context.Tasks.Find(taskId);
            task.Active = false;
            context.SaveChanges();
        }
    }
}
