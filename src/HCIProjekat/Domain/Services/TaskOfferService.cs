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

        public TaskOfferService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
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

            return context.TaskOffers.Find(id);
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
            var task = context.Tasks.Find(offer.Task.Id);
            
            //var newStatus = offer.OfferStatus;
            switch (offer.OfferStatus)
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


            context.SaveChanges();

            return offer;
        }

        public void AcceptTaskOffer(int taskId, int taskOfferId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var task = context.Tasks
                              .Include(t => t.Offers)
                              .Where(t => t.Id == taskId)
                              .First();

            var offers = task.Offers;

            foreach (var offer in offers)
            {
                if (offer.Id == taskOfferId)
                    offer.OfferStatus = OfferStatus.ACCEPTED;
                else
                    offer.OfferStatus = OfferStatus.REJECTED;
            }
            task.TaskStatus = TaskStatus.ACCEPTED;

            context.SaveChanges();
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

        public TaskOffer RejectTaskOffer(int taskId, int taskOfferId)
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
            return offer;
        }
    }
}
