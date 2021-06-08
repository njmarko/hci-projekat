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
    public class ClientService : IClientService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public ClientService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Client Create(Client client)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Users.FirstOrDefault(u => u.Username == client.Username) != null)
            {
                throw new UsernameAlreadyExistsException(client.Username);
            }

            context.Clients.Add(client);
            context.SaveChanges();
            return client;
        }

        public void Delete(int clientId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var client = context.Clients.Find(clientId);
            client.Active = false;
            context.SaveChanges();
        }

        public Client Get(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Clients.Find(id);
        }

        public Page<Client> GetClients(ClientsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Clients
                          .Where(c => c.Active)
                          .Where(c => c.Username.ToLower().Contains(page.Query.ToLower())
                          || c.FirstName.ToLower().Contains(page.Query.ToLower())
                          || c.LastName.ToLower().Contains(page.Query.ToLower()))
                          .Where(c => c.DateOfBirth <= page.BornBefore || !page.BornBefore.HasValue)
                          .Where(c => c.DateOfBirth >= page.BornAfter || !page.BornAfter.HasValue)
                          .Where(c => (c.MyRequests.Count() > 0) || !page.HasActiveRequests)
                          .Include(c => c.MyRequests)
                          .ToPage(page);
        }

        public Page<Request> GetRequestsForClient(int clientId, RequestsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var query = page.Query.ToLower();
            return context.Requests
                          .Where(r => r.Active)
                          .Where(r => r.Client.Id == clientId)
                          .Where(r => page.Type == null || r.Type == page.Type)
                          .Where(r => page.From == null || r.Date >= page.From)
                          .Where(r => page.To == null || r.Date <= page.To)
                          .Where(r => r.Name.ToLower().Contains(query)
                          || r.Notes.ToLower().Contains(query)
                          || r.Type.ToString().ToLower().Contains(query)
                          || r.Theme.ToLower().Contains(query))
                          .ToPage(page);
        }

        public Client Update(Client client)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Clients.Update(client);
            context.SaveChanges();

            return client;
        }
    }
}
