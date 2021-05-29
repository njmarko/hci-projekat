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

        public Page<TaskOffer> GetOffersForTask(int taskId, OffersForTaskPageRequest page)
        {
            var context = _dbContextFactory.CreateDbContext();
            return context.TaskOffers
                          .Include(to => to.Offer)
                          .Include(to => to.Offer.Partner)
                          .Where(o => o.Task.Id == taskId)
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

            return context.Offers
                          .Where(o => o.Partner.Id == partnerId)
                          .Where(o => o.Name.ToLower().Contains(page.OfferName.ToLower()))
                          .ToPage(page);
        }
    }
}
