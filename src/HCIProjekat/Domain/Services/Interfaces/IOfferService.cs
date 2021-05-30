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
    public interface IOfferService
    {
        Page<TaskOffer> GetOffersForTask(int taskId, OffersForTaskPageRequest page);

        Page<Offer> GetOffersForPartner(int partnerId, OffersPage page);

        Offer Get(int offerId);

        Offer Create(Offer offer, int partnerId);

        Offer Update(Offer offer);

        void Delete(int offerId);
    }
}
