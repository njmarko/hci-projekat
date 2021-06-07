using Domain.Entities;
using Domain.Exceptions;
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

        public EventPlanner Create(EventPlanner eventPlanner)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Users.FirstOrDefault(u => u.Username == eventPlanner.Username) != null)
            {
                throw new PartnerAlreadyExistsException(eventPlanner.Username);
            }

            context.EventPlanners.Add(eventPlanner);
            context.SaveChanges();
            return eventPlanner;
        }

        public void Delete(int eventPlannerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var eventPlanner = context.EventPlanners.Find(eventPlannerId);
            eventPlanner.Active = false;
            context.SaveChanges();
        }

        public List<Request> GetActiveRequests(int eventPlannerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Requests
                          .Where(r => r.Active)
                          .Where(r => r.EventPlanner != null && r.EventPlanner.Id == eventPlannerId)    // TODO: Ubaci logiku za to da li je zahtev vec obradjen kad stavimo to u model
                          .ToList();
        }

        public Page<EventPlanner> GetEventPlanners(EventPlannersPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.EventPlanners
                          .Where(c => c.Active)
                          .Where(c => c.Username.ToLower().Contains(page.Query.ToLower())
                          || c.FirstName.ToLower().Contains(page.Query.ToLower())
                          || c.LastName.ToLower().Contains(page.Query.ToLower()))
                          .Where(c => c.DateOfBirth <= page.BornBefore || !page.BornBefore.HasValue)
                          .Where(c => c.DateOfBirth >= page.BornAfter || !page.BornAfter.HasValue)
                          .Where(c => (c.AcceptedRequests.Count() > 0) || !page.HasActiveRequests)
                          .Include(c => c.AcceptedRequests)
                          .ToPage(page);
        }
    }
}
