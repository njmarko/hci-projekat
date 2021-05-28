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
    public class PartnersService : IPartnersService
    {

        private readonly IApplicationDbContextFactory _dbContextFactory;

        public PartnersService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public Page<Partner> GetClients(PartnersPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Partner
                          .Where(p => p.Name.ToLower().Contains(page.SearchQuery.ToLower())
                          || p.Location.City.ToLower().Contains(page.SearchQuery.ToLower())
                          || p.Location.Country.ToLower().Contains(page.SearchQuery.ToLower())
                          || p.Location.Street.ToLower().Contains(page.SearchQuery.ToLower())
                          || p.Location.StreetNumber.ToLower().Contains(page.SearchQuery.ToLower())
                          )
                          .Include(c => c.Offers)
                          .ToPage(page);
        }
    }
}
