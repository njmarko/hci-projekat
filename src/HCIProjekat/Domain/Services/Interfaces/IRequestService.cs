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

        Request GetWithGuests(int requestId);

        int GetRequestCost(int requestId);

        bool IsLocationAccepted(int requestId);

        SeatingLayout GetSeatingLayout(int requestId);

        Guest AddGuest(Guest guest, int requestId);

        void RemoveGuest(int requestId, int guestId);
    }
}
