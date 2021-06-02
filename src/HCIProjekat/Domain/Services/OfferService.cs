using Domain.Entities;
using Domain.Pagination;
using Domain.Pagination.Requests;
using Domain.Persistence;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OfferService : IOfferService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public OfferService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        
      
       public Offer Create(Offer offer, int partnerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var partner = context.Partner.Find(partnerId);
            offer.Partner = partner;
            context.Offers.Add(offer);
            context.SaveChanges();
            return offer;
        }

        public Page<Offer> GetOffersForPartner(int partnerId, OffersPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var searchQuery = page.SearchQuery.ToLower();

            return context.Offers
                          .Where(o => o.Active)
                          .Where(o => o.Partner.Id == partnerId)
                          .Where(o => o.Name.ToLower().Contains(searchQuery) || 
                                      o.Description.ToLower().Contains(searchQuery) ||
                                      o.Price.ToString().Contains(searchQuery))
                          .Where(o => page.OfferType == null || o.OfferType == page.OfferType.Value)
                          .ToPage(page);
        }

        public Page<Offer> GetAvailableOffersForTask(int taskId, OffersForTaskPageRequest page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var searchQuery = page.SearchQuery.ToLower();
            var task = context.Tasks.Where(t => t.Active)
                                    .Where(t => t.Id == taskId)
                                    .Include(t => t.Offers)
                                    .First();

            var offerIds = task.Offers.Select(to => to.Offer.Id);

            return context.Offers
                          .Where(o => o.Active)
                          .Include(o => o.Partner)
                          .Where(o => o.Partner.Active)
                          .Where(o => o.OfferType == task.TaskType)
                          .Where(o => !offerIds.Contains(o.Id))
                          .Where(o => o.Name.ToLower().Contains(searchQuery) ||
                                    o.Description.ToLower().Contains(searchQuery) ||
                                    o.Price.ToString().Contains(searchQuery))
                          .ToPage(page);
        }

        public Offer Get(int offerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Offers.Find(offerId);
        }

        public Offer Update(Offer offer)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Offers.Update(offer);
            context.SaveChanges();

            return offer;
        }

        public void Delete(int offerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var offer = context.Offers.Find(offerId);

            offer.Active = false;

            context.SaveChanges();
        }
    }
}
