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

        public Request GetWithGuests(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Requests
                          .Include(r => r.Guests)
                          .SingleOrDefault(r => r.Id == requestId);
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

        public bool IsLocationAccepted(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var task = context.Tasks
                              .FirstOrDefault(t => t.Request.Id == requestId && t.TaskType == ServiceType.LOCATION && t.TaskStatus == TaskStatus.ACCEPTED);
            return task != null;
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

            context.Requests.Update(request);
            context.SaveChanges();
            return request;
        }

        public SeatingLayout GetSeatingLayout(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            //var offer = context.TaskOffers
            //                   .Include(to => to.Offer)
            //                   .ThenInclude(o => o.SeatingLayout)
            //                   .Where(to => to.Offer.OfferType == ServiceType.LOCATION)
            //                   .Where(to => to.OfferStatus == OfferStatus.ACCEPTED)
            //                   .SingleOrDefault(to => to.Task.Request.Id == requestId);
            //if (offer == null)
            //{
            //    return null;
            //}

            //var layout = context.SeatingLayouts
            //                    .Include(l => l.Tables)
            //                    .ThenInclude(t => t.Chairs)
            //                    .SingleOrDefault(l => l.Id == offer.Offer.SeatingLayout.Id);
            var request = context.Requests.Include(r => r.SeatingLayout).ThenInclude(l => l.Tables).ThenInclude(t => t.Chairs).SingleOrDefault(r => r.Id == requestId);
            return request.SeatingLayout;
        }

        public Guest AddGuest(Guest guest, int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests.Include(r => r.Guests).SingleOrDefault(r => r.Id == requestId);
            request.Guests.Add(guest);
            context.SaveChanges();

            return guest;
        }

        public void RemoveGuest(int requestId, int guestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests.Include(r => r.Guests).SingleOrDefault(r => r.Id == requestId);
            var guest = context.Guests.Find(guestId);
            request.Guests.Remove(guest);
            context.SaveChanges();
        }

        public Guest SetGuestChair(int guestId, int chairId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var guest = context.Guests.Find(guestId);
            guest.ChairId = chairId;
            context.SaveChanges();

            return guest;
        }
    }
}
