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
    public class ClientService : IClientService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public ClientService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Page<Client> GetClients(ClientsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Clients
                          .Where(c => c.Username.ToLower().Contains(page.SearchQuery.ToLower()))
                          .ToPage(page);
        }

        public Page<Request> GetRequestsForClient(int clientId, RequestsPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Requests
                          .Where(r => r.Client.Id == clientId)
                          .Where(r => r.Name.ToLower().Contains(page.RequestName.ToLower()))
                          .ToPage(page);
        }

     }
}
