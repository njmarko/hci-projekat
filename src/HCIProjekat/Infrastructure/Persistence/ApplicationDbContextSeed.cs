using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


            var t1 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 1", Description = "ovo je neki opis taska", Request = r1 };
            var t2 = new Task { TaskStatus = TaskStatus.REJECTED, TaskType = ServiceType.CATERING, Name = "Task 2", Description = "ovo je neki opis taska", Request = r2 };
            var t3 = new Task { TaskStatus = TaskStatus.IN_PROGRESS, TaskType = ServiceType.CATERING, Name = "Task 3", Description = "ovo je neki opis taska", Request = r1 };
            var t4 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 4", Description = "ovo je neki opis taska", Request = r1 };
            var t5 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 5", Description = "ovo je neki opis taska", Request = r1 };
            var t6 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 6", Description = "ovo je neki opis taska", Request = r2 };
            var t7 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 7", Description = "ovo je neki opis taska", Request = r1 };
            var t8 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 8", Description = "ovo je neki opis taska", Request = r2 };
            var t9 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 9", Description = "ovo je neki opis taska", Request = r1 };
            var t10 = new Task { TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.CATERING, Name = "Task 10", Description = "ovo je neki opis taska", Request = r1 };


            var l1 = new Location { StreetNumber = "301", Street = "Ulica1", City = "Novi Sad", Country = "Srbija" };
            var l2 = new Location { StreetNumber = "302", Street = "Ulica2", City = "Novi Sad", Country = "Srbija" };
            var l3 = new Location { StreetNumber = "303", Street = "Ulica3", City = "Novi Sad", Country = "Srbija" };

            var p1 = new Partner { Name = "Partner 1", Location = l1, Type = PartnerType.RESTAURANT };
            var p2 = new Partner { Name = "Partner 2", Location = l2, Type = PartnerType.RESTAURANT };

            var o1 = new Offer { Name = "Ponuda 1", Price = 1000, Description = "opis ponude", Image = "slika", OfferType = t1.TaskType, Partner = p1 };
            var o2 = new Offer { Name = "Ponuda 2", Price = 2000, Description = "opis ponude 2", Image = "slika", OfferType = t1.TaskType, Partner = p2 };

            var taskOffer1 = new TaskOffer { Offer = o1, Task = t1 };
            var taskOffer2 = new TaskOffer { Offer = o2, Task = t1 };


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

            context.Tasks.Add(t1);
            context.Tasks.Add(t2);
            context.Tasks.Add(t3);
            context.Tasks.Add(t4);
            context.Tasks.Add(t5);
            context.Tasks.Add(t6);
            context.Tasks.Add(t7);
            context.Tasks.Add(t8);
            context.Tasks.Add(t9);
            context.Tasks.Add(t10);

            context.Partner.Add(p1);
            context.Partner.Add(p2);

            context.Offers.Add(o1);
            context.Offers.Add(o2);

            context.TaskOffers.Add(taskOffer1);
            context.TaskOffers.Add(taskOffer2);


            context.SaveChanges();
        }
    }
}
