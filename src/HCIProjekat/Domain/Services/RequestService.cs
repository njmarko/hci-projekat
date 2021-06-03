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
    public class RequestService : IRequestService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;
        private readonly INotificationService _notificationService;
        public RequestService(IApplicationDbContextFactory dbContextFactory, INotificationService notificationService)
        {
            _dbContextFactory = dbContextFactory;
            _notificationService = notificationService;
        }

        public Request Accept(int requestId, int eventPlannerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests.Include(r => r.Client).SingleOrDefault(r => r.Id == requestId);
            var eventPlanner = context.EventPlanners.Find(eventPlannerId);
            request.EventPlanner = eventPlanner;

            context.Requests.Update(request);
            context.SaveChanges();

            // Notify a client that his request has been accepted
            var notification = new Notification { Message = $"Your request has been accepted by {eventPlanner.FirstName} {eventPlanner.LastName}.", UserId = request.Client.Id, RequestId = requestId };
            _notificationService.Push(notification);

            return request;
        }

        public Request Create(int clientId, Request request)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var client = context.Clients.Find(clientId);
            request.Client = client;

            context.Requests.Add(request);
            context.SaveChanges();

            // Notify all the event planners about the new request
            foreach (var planner in context.EventPlanners.Where(e => e.Active))
            {
                var notification = new Notification { Message = $"New request has been made.", UserId = planner.Id, RequestId = request.Id };
                _notificationService.Push(notification);
            }

            return request;
        }

        public void Delete(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests.Find(requestId);
            request.Active = false;
            context.SaveChanges();
        }

        public Request Get(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Requests.Find(id);
        }

        public Page<Request> GetAllRequests(RequestsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var query = page.Query.ToLower();
            return context.Requests
                          .Include(r => r.Client)
                          .Include(r => r.EventPlanner)
                          .Where(r => r.Active)
                          .Where(r => page.Type == null || r.Type == page.Type)
                          .Where(r => page.From == null || r.Date >= page.From)
                          .Where(r => page.To == null || r.Date <= page.To)
                          .Where(r => r.Name.ToLower().Contains(query)
                          || r.Notes.ToLower().Contains(query)
                          || r.Type.ToString().ToLower().Contains(query)
                          || r.Theme.ToLower().Contains(query))
                          .ToPage(page);
        }

        public int GetRequestCost(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests
                                 .Include(r => r.Tasks)
                                 .ThenInclude(t => t.Offers)
                                 .ThenInclude(o => o.Offer)
                                 .SingleOrDefault(r => r.Id == requestId);
            
            var acceptedTasks = request.Tasks
                .Where(t => t.TaskStatus == TaskStatus.ACCEPTED);
            int cost = 0;
            
            foreach (var task in acceptedTasks)
            {
                var offers = task.Offers
                    .Where(o => o.OfferStatus == OfferStatus.ACCEPTED);
                
                if (offers.Any())
                {
                    var offer = offers.First();
                    cost += offer.Offer.Price;

                }
            }
            return cost;
            
        }

        public Page<Request> GetRequestInterestingForEventPlanner(int eventPlannerId, EventPlannerRequestsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var query = page.Query.ToLower();
            var requests = context.Requests
                                   .Where(r => r.Active)
                                   .Where(r => r.EventPlanner == null || r.EventPlanner.Id == eventPlannerId)
                                   .Where(r => page.Type == null || r.Type == page.Type)
                                   .Where(r => page.From == null || r.Date >= page.From)
                                   .Where(r => page.To == null || r.Date <= page.To)
                                   .Where(r => r.Name.ToLower().Contains(query)
                                    || r.Notes.ToLower().Contains(query)
                                    || r.Type.ToString().ToLower().Contains(query)
                                    || r.Theme.ToLower().Contains(query));
            if (page.Mine)
            {
                requests = requests.Where(r => r.EventPlanner != null && r.EventPlanner.Id == eventPlannerId);
            }
            if (page.OnlyNew)
            {
                requests = requests.Where(r => r.EventPlanner == null);
            }
            return requests.Include(r => r.EventPlanner).ToPage(page);
        }

        public Request Reject(int requestId, int eventPlannerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests.Include(r => r.Client).SingleOrDefault(r => r.Id == requestId);
            var eventPlanner = context.EventPlanners.Find(eventPlannerId);
            //eventPlanner.AcceptedRequests.Remove(request);  

            request.EventPlanner = null;
            context.Requests.Update(request);
            context.SaveChanges();

            // Notify a client that his request has been canceled
            var notification = new Notification { Message = $"Your request has been canceled by {eventPlanner.FirstName} {eventPlanner.LastName}.", UserId = request.Client.Id, RequestId = requestId };
            _notificationService.Push(notification);

            return request;
        }

        public Request Update(Request request)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var req = context.Requests.Find(request.Id);
            var planner = context.EventPlanners
                                 .Include(e => e.AcceptedRequests)
                                 .SingleOrDefault(e => e.AcceptedRequests.Contains(req));

            if (planner != null && request.EventPlanner == null)
            {
                planner.AcceptedRequests.Remove(req);
            }

            req.Name = request.Name;
            req.GuestNumber = request.GuestNumber;
            req.Budget = request.Budget;
            req.BudgetFlexible = request.BudgetFlexible;
            req.Theme = request.Theme;
            req.Notes = request.Notes;
            req.Type = request.Type;
            context.Requests.Update(req);

            context.SaveChanges();
            return request;
        }
    }
}
