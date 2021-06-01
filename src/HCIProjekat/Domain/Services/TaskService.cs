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

        public TaskService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void AcceptTaskOffer(int taskId, int taskOfferId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var task = context.Tasks
                              .Include(t => t.Offers)
                              .Where(t => t.Id == taskId)
                              .First();

            var offers = task.Offers;
            
            foreach(var offer in offers)
            {
                if (offer.Id == taskOfferId)
                    offer.OfferStatus = OfferStatus.ACCEPTED;
                else
                    offer.OfferStatus = OfferStatus.REJECTED;
            }
            task.TaskStatus = TaskStatus.ACCEPTED;

            context.SaveChanges();
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
                          .Where(t => t.Name.ToLower().Contains(searchQuery)
                          || t.Description.ToLower().Contains(searchQuery)
                          || t.TaskType.ToString().ToLower().Contains(searchQuery))
                          .ToList();
        }

        public Task Update(Task task)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }
        public void RejectAllTaskOffers(int taskId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var task = context.Tasks
                              .Include(t => t.Offers)
                              .Where(t => t.Id == taskId)
                              .First();
            foreach (var offer in task.Offers)
                offer.OfferStatus = OfferStatus.REJECTED;
            task.TaskStatus = TaskStatus.REJECTED;
            context.SaveChanges();
        }

        public void RejectTaskOffer(int taskId, int taskOfferId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var task = context.Tasks
                              .Include(t => t.Offers)
                              .Where(t => t.Id == taskId)
                              .First();
            
           var offer = task.Offers
                           .Where(o => o.Id == taskOfferId)
                           .First();
            
            offer.OfferStatus = OfferStatus.REJECTED;
            if (task.Offers.Where(offer => offer.OfferStatus == OfferStatus.REJECTED).Count() == task.Offers.Count())
                task.TaskStatus = TaskStatus.REJECTED;
            
            context.SaveChanges();
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
