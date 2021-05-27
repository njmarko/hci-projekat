using Domain.Entities;
using Domain.Pagination;
using Domain.Pagination.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IClientService
    {
        Page<Request> GetRequestsForClient(int clientId, RequestsPage page);

        public Page<Client> GetClients(ClientsPage page);
    }
}
