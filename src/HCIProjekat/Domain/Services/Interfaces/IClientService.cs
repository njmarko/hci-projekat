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
        Client Create(Client client);

        Page<Request> GetRequestsForClient(int clientId, RequestsPage page);

        Page<Client> GetClients(ClientsPage page);

        public void Delete(int clientId);
    }
}
