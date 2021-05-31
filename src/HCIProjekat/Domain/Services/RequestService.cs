﻿using Domain.Entities;
using Domain.Pagination;
using Domain.Pagination.Requests;
using Domain.Persistence;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class RequestService : IRequestService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public RequestService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Request Create(int clientId, Request request)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var client = context.Clients.Find(clientId);
            request.Client = client;

            context.Requests.Add(request);
            context.SaveChanges();
            return request;
        }

        public Request GetRequest(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Requests.Find(requestId);
        }

        public Page<Request> GetRequestInterestingForEventPlanner(int eventPlannerId, EventPlannerRequestsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Requests
                          .Where(r => page.OnlyNew && r.EventPlanner == null)
                          .Where(r => page.Mine && r.EventPlanner != null && r.EventPlanner.Id == eventPlannerId)
                          .Where(r => page.Type == null || r.Type == page.Type)
                          .ToPage(page);
        }
    }
}
