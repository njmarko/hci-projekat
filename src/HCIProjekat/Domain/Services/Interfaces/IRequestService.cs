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
    public interface IRequestService
    {
        Request Create(int clientId, Request request);

        Request GetRequest(int requestId);

        Request Accept(int requestId, int eventPlannerId);

        Request Reject(int requestId, int eventPlannerId);

        Request Update(Request request);

        void Delete(int requestId);

        Page<Request> GetRequestInterestingForEventPlanner(int eventPlannerId, EventPlannerRequestsPage page);
    }
}
