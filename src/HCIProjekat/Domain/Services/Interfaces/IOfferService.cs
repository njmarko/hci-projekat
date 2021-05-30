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
    public interface IOfferService : ICRUDService<Offer>
    {
        Page<TaskOffer> GetOffersForTask(int taskId, OffersForTaskPageRequest page);

        Page<Offer> GetOffersForPartner(int partnerId, OffersPage page);

        Offer Create(Offer offer, int partnerId);
    }
}
