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
            var c8 = new Client { FirstName = "Dejan7", LastName = "Djordjevic7", Username = "dejandjordjevic7", Password = "test123", DateOfBirth = DateTime.Now.AddDays(1) };
            context.Clients.Add(c1);
            context.Clients.Add(c2);
            context.Clients.Add(c3);
            context.Clients.Add(c4);
            context.Clients.Add(c5);
            context.Clients.Add(c6);
            context.Clients.Add(c7);
            context.Clients.Add(c8);

            var r1 = new Request { Name = "Request 1", Budget = 1000, BudgetFlexible = true, Client = c1, Date = DateTime.Now, GuestNumber = 12, Type = RequestType.PARTY, Theme = "Theme", Notes = "No notes." };
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
