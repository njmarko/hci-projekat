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
    public interface IEventPlannersService
    {
        Page<EventPlanner> GetEventPlanners(EventPlannersPage page);

        EventPlanner Create(EventPlanner eventPlanner);

        List<Request> GetActiveRequests(int eventPlannerId);

        public void Delete(int eventPlannerId);
    }
}
