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
    public class EventPlannersService : IEventPlannersService
    {

        private readonly IApplicationDbContextFactory _dbContextFactory;

        public EventPlannersService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Page<EventPlanner> GetEventPlanners(EventPlannersPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.EventPlanners
                          .Where(c => c.Username.ToLower().Contains(page.SearchQuery.ToLower())
                          || c.FirstName.ToLower().Contains(page.SearchQuery.ToLower())
                          || c.LastName.ToLower().Contains(page.SearchQuery.ToLower())
                          || c.DateOfBirth.ToString().ToLower().Contains(page.SearchQuery.ToLower()))
                          .Include(c => c.AcceptedRequests)
                          .ToPage(page);
        }
    }
}
