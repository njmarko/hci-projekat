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
    public interface IRequestService : ICRUDService<Request>
    {
        Request Create(int clientId, Request request);

        Request Accept(int requestId, int eventPlannerId);

        Request Reject(int requestId, int eventPlannerId);

        Page<Request> GetRequestInterestingForEventPlanner(int eventPlannerId, EventPlannerRequestsPage page);

        Page<Request> GetAllRequests(RequestsPage page);

        int GetRequestCost(int requestId);
    }
}
