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

            return context.TaskOffers
                .Include(to => to.Task)
                .Where(to => to.Id == id)
                .First();
        }

        public Page<TaskOffer> GetOffersForTask(int taskId, OffersForTaskPageRequest page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var searchQuery = page.SearchQuery.ToLower();

            return context.TaskOffers
                          .Include(to => to.Offer)
                          .Include(to => to.Offer.Partner)
                          .Where(to => to.Active)
                          .Where(to => to.Task.Id == taskId)
                          .Where(to => to.Offer.Name.ToLower().Contains(searchQuery) ||
                                    to.Offer.Description.ToLower().Contains(searchQuery) ||
                                    to.Offer.Price.ToString().Contains(searchQuery))
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
            //return offer;
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

        public TaskOffer AddOfferForTask(int taskId, int offerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var offer = context.Offers
                               .Where(o => o.Active)
                               .Where(o => o.Id == offerId)
                               .First();

            var task = context.Tasks
                              .Where(t => t.Active)
                              .Include(t => t.Offers)
                              .Where(t => t.Id == taskId)
                              .First();

            var taskOffer = new TaskOffer { OfferStatus = OfferStatus.PENDING, Offer = offer, Task = task };
            context.TaskOffers.Add(taskOffer);

            context.SaveChanges();

            return taskOffer;
        }

        public void RemoveOfferFromTask(int taskOfferId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var taskOffer = context.TaskOffers
                                   .Where(to => to.Active)
                                   .Where(to => to.Id == taskOfferId)
                                   .First();

            taskOffer.Active = false;

            context.SaveChanges();
        }
    }
}
