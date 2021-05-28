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
            context.Clients.Add(c1);

            var r1 = new Request { Name = "Request 1", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Neka tema", Notes = "No notes." };
            var r2 = new Request { Name = "Request 2", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r3 = new Request { Name = "Request 3", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r4 = new Request { Name = "Request 4", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r5 = new Request { Name = "Request 5", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r6 = new Request { Name = "Request 6", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r7 = new Request { Name = "Request 7", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r8 = new Request { Name = "Request 8", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r9 = new Request { Name = "Request 9", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r10 = new Request { Name = "Request 10", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r11 = new Request { Name = "Request 11", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r12 = new Request { Name = "Request 12", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r13 = new Request { Name = "Request 13", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
            var r14 = new Request { Name = "Request 14", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
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

            context.SaveChanges();
        }
    }
}
