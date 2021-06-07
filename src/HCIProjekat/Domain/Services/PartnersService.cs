using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Pagination;
using Domain.Pagination.Requests;
using Domain.Persistence;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PartnersService : IPartnersService
    {

        private readonly IApplicationDbContextFactory _dbContextFactory;

        public PartnersService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Partner Create(Partner partner)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Partners.FirstOrDefault(u => u.Name == partner.Name) != null)
            {
                throw new PartnerAlreadyExistsException(partner.Name);
            }

            context.Partners.Add(partner);
            context.SaveChanges();
            return partner;
        }

        public void Delete(int partnerId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var partner = context.Partners.Find(partnerId);
            partner.Active = false;
            context.SaveChanges();
        }

        public Page<Partner> GetPartners(PartnersPage page)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Partners
                          .Where(p => p.Active)
                          .Where(p => p.Name.ToLower().Contains(page.Query.ToLower())
                          || p.Location.City.ToLower().Contains(page.Query.ToLower())
                          || p.Location.Country.ToLower().Contains(page.Query.ToLower())
                          || p.Location.Street.ToLower().Contains(page.Query.ToLower())
                          || p.Location.StreetNumber.ToLower().Contains(page.Query.ToLower())
                          )
                          .Where(p => p.Type == page.PartnerType || page.PartnerType == null)
                          .Include(c => c.Offers)
                          .ToPage(page);
        }
        public Partner Update(int partnerId, string name, PartnerType partnerType, string country, string city, string street, string streetNumber)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var partner = context.Partners.Find(partnerId);
            partner.Name = name;
            partner.Type = partnerType;
            partner.Location.Country = country;
            partner.Location.City = city;
            partner.Location.Street = street;
            partner.Location.StreetNumber = streetNumber;
            context.SaveChanges();
            return partner;
        }

    }
}
