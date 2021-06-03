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
    public interface ITaskOfferService : ICRUDService<TaskOffer>
    {
        Page<TaskOffer> GetOffersForTask(int taskId, OffersForTaskPageRequest page);

        List<TaskOffer> GetAllTaskOffersForTask(int taskId);

        TaskOffer AddOfferForTask(int taskId, int offerId);

        void RemoveOfferFromTask(int taskOfferId);

        void RejectAllTaskOffers(int taskId);

        void RejectTaskOffer(int taskId, int taskOfferId);

        void AcceptTaskOffer(int taskId, int taskOfferId);
    }
}
