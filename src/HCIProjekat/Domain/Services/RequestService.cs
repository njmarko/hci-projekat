using Domain.Entities;
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

        public Request GetRequest(int requestId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Requests.Find(requestId);
        }
    }
}
