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
                .ToPage(page);
        }
    }
}
