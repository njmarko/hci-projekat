using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(ApplicationDbContext context)
        {
            var c1 = new Client { FirstName = "Dejan", LastName = "Djordjevic", Username = "dejandjordjevic", Password = "test123", DateOfBirth = DateTime.Now };
            var c2 = new Client { FirstName = "Dejan1", LastName = "Djordjevic1", Username = "dejandjordjevic1", Password = "test123", DateOfBirth = DateTime.Now.AddDays(1) };
            var c3 = new Client { FirstName = "Dejan2", LastName = "Djordjevic2", Username = "dejandjordjevic2", Password = "test123", DateOfBirth = DateTime.Now.AddDays(2) };
            var c4 = new Client { FirstName = "Dejan3", LastName = "Djordjevic3", Username = "dejandjordjevic3", Password = "test123", DateOfBirth = DateTime.Now.AddDays(3) };
            var c5 = new Client { FirstName = "Dejan4", LastName = "Djordjevic4", Username = "dejandjordjevic4", Password = "test123", DateOfBirth = DateTime.Now.AddDays(4) };
            var c6 = new Client { FirstName = "Dejan5", LastName = "Djordjevic5", Username = "dejandjordjevic5", Password = "test123", DateOfBirth = DateTime.Now.AddDays(5) };
            var c7 = new Client { FirstName = "Dejan6", LastName = "Djordjevic6", Username = "dejandjordjevic6", Password = "test123", DateOfBirth = DateTime.Now.AddDays(6) };
            var c8 = new Client { FirstName = "Dejan7", LastName = "Djordjevic7", Username = "dejandjordjevic7", Password = "test123", DateOfBirth = DateTime.Now.AddDays(7) };
            var c9 = new Client { FirstName = "Dejan8", LastName = "Djordjevic8", Username = "dejandjordjevic8", Password = "test123", DateOfBirth = DateTime.Now.AddDays(8) };
            var c10 = new Client { FirstName = "Dejan9", LastName = "Djordjevic9", Username = "dejandjordjevic9", Password = "test123", DateOfBirth = DateTime.Now.AddDays(9) };
            context.Clients.Add(c1);
            context.Clients.Add(c2);
            context.Clients.Add(c3);
            context.Clients.Add(c4);
            context.Clients.Add(c5);
            context.Clients.Add(c6);
            context.Clients.Add(c7);
            context.Clients.Add(c8);
            context.Clients.Add(c9);
            context.Clients.Add(c10);

            var r1 = new Request { Name = "Request 1", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r2 = new Request { Name = "Request 2", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r3 = new Request { Name = "Request 3", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r4 = new Request { Name = "Request 4", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r5 = new Request { Name = "Request 5", Budget = 1000, BudgetFlexible = true, Client = c2, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r6 = new Request { Name = "Request 6", Budget = 1000, BudgetFlexible = true, Client = c2, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r7 = new Request { Name = "Request 7", Budget = 1000, BudgetFlexible = true, Client = c2, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r8 = new Request { Name = "Request 8", Budget = 1000, BudgetFlexible = true, Client = c3, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r9 = new Request { Name = "Request 9", Budget = 1000, BudgetFlexible = true, Client = c3, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r10 = new Request { Name = "Request 10", Budget = 1000, BudgetFlexible = true, Client = c4, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r11 = new Request { Name = "Request 11", Budget = 1000, BudgetFlexible = true, Client = c4, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r12 = new Request { Name = "Request 12", Budget = 1000, BudgetFlexible = true, Client = c5, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r13 = new Request { Name = "Request 13", Budget = 1000, BudgetFlexible = true, Client = c6, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r14 = new Request { Name = "Request 14", Budget = 1000, BudgetFlexible = true, Client = c7, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            context.Requests.Add(r1);
            context.Requests.Add(r2);
            context.Requests.Add(r3);
            context.Requests.Add(r4);
            context.Requests.Add(r5);
            context.Requests.Add(r6);
            context.Requests.Add(r7);
            context.Requests.Add(r8);
            context.Requests.Add(r9);
            context.Requests.Add(r10);
            context.Requests.Add(r11);
            context.Requests.Add(r12);
            context.Requests.Add(r13);
            context.Requests.Add(r14);


            var ep1 = new EventPlanner { FirstName = "Pera1", LastName = "Peric1", Username = "peraperic1", Password = "test123", DateOfBirth = DateTime.Now.AddDays(1), AcceptedRequests = new List<Request>() };
            var ep2 = new EventPlanner { FirstName = "Pera2", LastName = "Peric2", Username = "peraperic2", Password = "test123", DateOfBirth = DateTime.Now.AddDays(2), AcceptedRequests = new List<Request>() };
            var ep3 = new EventPlanner { FirstName = "Pera3", LastName = "Peric3", Username = "peraperic3", Password = "test123", DateOfBirth = DateTime.Now.AddDays(3), AcceptedRequests = new List<Request>() };
            var ep4 = new EventPlanner { FirstName = "Pera4", LastName = "Peric4", Username = "peraperic4", Password = "test123", DateOfBirth = DateTime.Now.AddDays(4), AcceptedRequests = new List<Request>() };
            var ep5 = new EventPlanner { FirstName = "Pera5", LastName = "Peric5", Username = "peraperic5", Password = "test123", DateOfBirth = DateTime.Now.AddDays(5), AcceptedRequests = new List<Request>() };
            var ep6 = new EventPlanner { FirstName = "Pera6", LastName = "Peric6", Username = "peraperic6", Password = "test123", DateOfBirth = DateTime.Now.AddDays(6), AcceptedRequests = new List<Request>() };
            var ep7 = new EventPlanner { FirstName = "Pera7", LastName = "Peric7", Username = "peraperic7", Password = "test123", DateOfBirth = DateTime.Now.AddDays(7), AcceptedRequests = new List<Request>() };
            var ep8 = new EventPlanner { FirstName = "Pera8", LastName = "Peric8", Username = "peraperic8", Password = "test123", DateOfBirth = DateTime.Now.AddDays(8), AcceptedRequests = new List<Request>() };
            var ep9 = new EventPlanner { FirstName = "Pera9", LastName = "Peric9", Username = "peraperic9", Password = "test123", DateOfBirth = DateTime.Now.AddDays(9), AcceptedRequests = new List<Request>() };
            ep1.AcceptedRequests.Add(r1);
            ep1.AcceptedRequests.Add(r2);
            ep1.AcceptedRequests.Add(r3);
            ep1.AcceptedRequests.Add(r4);
            ep2.AcceptedRequests.Add(r5);
            ep2.AcceptedRequests.Add(r6);
            ep2.AcceptedRequests.Add(r7);
            ep3.AcceptedRequests.Add(r8);

            context.EventPlanners.Add(ep1);
            context.EventPlanners.Add(ep2);
            context.EventPlanners.Add(ep3);
            context.EventPlanners.Add(ep4);
            context.EventPlanners.Add(ep5);
            context.EventPlanners.Add(ep6);
            context.EventPlanners.Add(ep7);
            context.EventPlanners.Add(ep8);
            context.EventPlanners.Add(ep9);

            // Locations
            var l1 = new Location { City = "Novi Sad", Country = "Serbia", Street = "Dunavska", StreetNumber = "11" };

            // Partners
            var par1 = new Partner { Name = "Prečenjara1", Type = PartnerType.RESTAURANT, Location = l1 };
            var par2 = new Partner { Name = "Prečenjara2", Type = PartnerType.RESTAURANT, Location = l1 };
            var par3 = new Partner { Name = "Prečenjara3", Type = PartnerType.RESTAURANT, Location = l1 };
            var par4 = new Partner { Name = "Prečenjara4", Type = PartnerType.RESTAURANT, Location = l1 };
            var par5 = new Partner { Name = "Prečenjara5", Type = PartnerType.RESTAURANT, Location = l1 };
            var par6 = new Partner { Name = "Prečenjara6", Type = PartnerType.RESTAURANT, Location = l1 };
            var par7 = new Partner { Name = "Prečenjara7", Type = PartnerType.RESTAURANT, Location = l1 };
            var par8 = new Partner { Name = "Prečenjara8", Type = PartnerType.RESTAURANT, Location = l1 };
            var par9 = new Partner { Name = "Prečenjara9", Type = PartnerType.RESTAURANT, Location = l1 };
            var par10 = new Partner { Name = "Prečenjara10", Type = PartnerType.RESTAURANT, Location = l1 };

            context.Partner.Add(par1);
            context.Partner.Add(par2);
            context.Partner.Add(par3);
            context.Partner.Add(par4);
            context.Partner.Add(par5);
            context.Partner.Add(par6);
            context.Partner.Add(par7);
            context.Partner.Add(par8);
            context.Partner.Add(par9);
            context.Partner.Add(par10);


            context.SaveChanges();
        }
    }
}
