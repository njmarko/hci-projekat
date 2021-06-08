using Domain.Entities;
using Domain.Enums;
using Domain.Pagination;
using Domain.Pagination.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IPartnersService
    {
        Page<Partner> GetPartners(PartnersPage page);

        Partner Create(Partner partner);

        public void Delete(int partnerId);

        Partner Update(int partnerId, string name, PartnerType partnerType, string country, string city, string street, string streetNumber);

        Partner GetPartner(int partnerId);
    }
}

