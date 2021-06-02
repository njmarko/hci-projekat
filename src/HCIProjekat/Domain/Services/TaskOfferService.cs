using Domain.Entities;
using Domain.Enums;
using Domain.Pagination;
using Domain.Pagination.Requests;
using Domain.Persistence;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class TaskOfferService : ITaskOfferService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;
        private readonly INotificationService _notificationService;

        public TaskOfferService(IApplicationDbContextFactory dbContextFactory, INotificationService notificationService)
        {
            _dbContextFactory = dbContextFactory;
            _notificationService = notificationService;
        }

        public void Delete(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var offer = context.TaskOffers.Find(id);
            offer.Active = false;

            context.SaveChanges();
        }

        public TaskOffer Get(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.TaskOffers
                          .Include(to => to.Task)
                          .Where(to => to.Id == id)
                          .First();
        }

        public Page<TaskOffer> GetOffersForTask(int taskId, OffersForTaskPageRequest page)
        {
            var context = _dbContextFactory.CreateDbContext();
            return context.TaskOffers
                          .Include(to => to.Offer)
                          .Include(to => to.Offer.Partner)
                          .Where(to => to.Active)
                          .Where(o => o.Task.Id == taskId)
                          .ToPage(page);
        }

        public TaskOffer Update(TaskOffer offer)
        {
            using var context = _dbContextFactory.CreateDbContext();
            
            context.TaskOffers.Update(offer);
            var task = context.Tasks
                              .Include(t => t.Offers)
                              .Where(t => t.Id == offer.Id)
                              .First();
            
            //var newStatus = offer.OfferStatus;
            /*switch (offer.OfferStatus)
            {
                case OfferStatus.PENDING:
                    task.TaskStatus = TaskStatus.SENT_TO_CLIENT;
                    break;
                case OfferStatus.ACCEPTED:
                    task.TaskStatus = TaskStatus.ACCEPTED;
                    break;
                case OfferStatus.REJECTED:
                    task.TaskStatus = TaskStatus.REJECTED;
                    break;

            }

            if(offer.OfferStatus == OfferStatus.REJECTED)
            {

            }*/


            context.SaveChanges();

            return offer;
        }

        public void AcceptTaskOffer(int taskId, int taskOfferId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var task = context.Tasks
                              .Include(t => t.Offers)
                              .Include(t => t.Request)
                              .ThenInclude(r => r.EventPlanner)
                              .SingleOrDefault(t => t.Id == taskId);

            var offers = task.Offers;

            foreach (var offer in offers)
            {
                if (offer.Id == taskOfferId)
                    offer.OfferStatus = OfferStatus.ACCEPTED;
                else
                    offer.OfferStatus = OfferStatus.REJECTED;
            }
            task.TaskStatus = TaskStatus.ACCEPTED;

            // Notify event planner that his task got accepted by the client
            var notification = new Notification { Message = "Your task has been accepted.", TaskId = taskId, UserId = task.Request.EventPlanner.Id };
            _notificationService.Push(notification);

            context.SaveChanges();
        }
        public void RejectAllTaskOffers(int taskId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var task = context.Tasks
                              .Include(t => t.Offers)
                              .Include(t => t.Request)
                              .ThenInclude(r => r.EventPlanner)
                              .SingleOrDefault(t => t.Id == taskId);
            foreach (var offer in task.Offers)
                offer.OfferStatus = OfferStatus.REJECTED;
            task.TaskStatus = TaskStatus.REJECTED;
            context.SaveChanges();

            // Notify event planner that his task got rejected by the client
            var notification = new Notification { Message = "Your task has been rejected.", TaskId = taskId, UserId = task.Request.EventPlanner.Id };
            _notificationService.Push(notification);
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
            var didReject = false;
            if (task.Offers.Count(offer => offer.OfferStatus == OfferStatus.REJECTED) == task.Offers.Count)
            {
                task.TaskStatus = TaskStatus.REJECTED;
                didReject = true;
            }

            context.SaveChanges();
            if (didReject)
            {
                // Notify event planner that his task got rejected by the client
                var notification = new Notification { Message = "Your task has been rejected.", TaskId = taskId, UserId = task.Request.EventPlanner.Id };
                _notificationService.Push(notification);
            }
        }

        public List<TaskOffer> GetAllTaskOffersForTask(int taskId)
        {
            //throw new NotImplementedException();
            using var context = _dbContextFactory.CreateDbContext();
            return context.TaskOffers
                .Include(to => to.Task)
                .Where(to => to.Task.Id == taskId)
                .ToList();
        }
    }
}
