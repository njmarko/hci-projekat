using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
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
      
       public Offer Create(Offer offer)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Offers.Add(offer);
            context.SaveChanges();
            return offer;
        }
    }
}
