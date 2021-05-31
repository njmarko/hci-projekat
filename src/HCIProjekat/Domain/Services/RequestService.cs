﻿using Domain.Entities;
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
    public class RequestService : IRequestService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public RequestService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Request Accept(int requestId, int eventPlannerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests.Find(requestId);
            var eventPlanner = context.EventPlanners.Find(eventPlannerId);
            request.EventPlanner = eventPlanner;

            context.Requests.Update(request);
            context.SaveChanges();
            return request;
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

        public void Delete(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var request = context.Requests.Find(requestId);
            request.Active = false;
            context.SaveChanges();
        }

        public Request GetRequest(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Requests.Find(requestId);
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

            var request = context.Requests.Find(requestId);
            var eventPlanner = context.EventPlanners.Find(eventPlannerId);
            eventPlanner.AcceptedRequests.Remove(request);  // VAZNO: mora eksplicitno da se obrise iz ove kolekcije jer nama je sve virutal pa on nema pojma ko je vlasnik ove kolekcije
                                                            // u sustini ti kolekcije nam samo smetaju tako da ih komotno mozemo i obrisati posto svakako svugde filitrimamo odgovarajuci DbSet
                                                            // a mozemo i proglasiti da su ovi kolekcije mapirane na odgovarajuce pojedinacne elemente ako ne bismo koristili virutal za tu vezu na strani pojedinacnog elementa
            request.EventPlanner = null;
            context.Requests.Update(request);
            context.SaveChanges();
            return request;
        }

        public Request Update(Request request)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Requests.Update(request);
            context.SaveChanges();
            return request;
        }
    }
}
