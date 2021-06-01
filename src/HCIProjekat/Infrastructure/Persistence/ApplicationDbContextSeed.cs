using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(ApplicationDbContext context)
        {
            var c1 = new Client { FirstName = "Dejan", LastName = "Djordjevic", Username = "dejandjordjevic", Password = "test123", DateOfBirth = DateTime.ParseExact("1999-04-04 14:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };
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

            var l1 = new Location { StreetNumber = "301", Street = "Ulica1", City = "Novi Sad", Country = "Srbija" };
            var l2 = new Location { StreetNumber = "302", Street = "Ulica2", City = "Novi Sad", Country = "Srbija" };
            var l3 = new Location { StreetNumber = "303", Street = "Ulica3", City = "Novi Sad", Country = "Srbija" };

            var p1 = new Partner { Name = "Partner 1", Location = l1, Type = PartnerType.RESTAURANT };
            var p2 = new Partner { Name = "Partner 2", Location = l2, Type = PartnerType.RESTAURANT };

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

            var ep1 = new EventPlanner { FirstName = "Jakov", LastName = "Matic", Username = "jakovmatic", Password = "test123", DateOfBirth = DateTime.Now, AcceptedRequests = new List<Request>() };
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

            // Partners
            var par1 = new Partner { Name = "Pečenjara1", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par2 = new Partner { Name = "Pečenjara2", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par3 = new Partner { Name = "Pečenjara3", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par4 = new Partner { Name = "Pečenjara4", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par5 = new Partner { Name = "Pečenjara5", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par6 = new Partner { Name = "Pečenjara6", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par7 = new Partner { Name = "Pečenjara7", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par8 = new Partner { Name = "Pečenjara8", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par9 = new Partner { Name = "Pečenjara9", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };
            var par10 = new Partner { Name = "Pečenjara10", Type = PartnerType.RESTAURANT, Location = new Location { City = "Novi Sad", Country = "Srbija", Street = "Dunavska", StreetNumber = "11" } };

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

            // Tasks
            var t1 = new Task { Name = "Task1", Description = "Neki opis", TaskStatus = TaskStatus.TO_DO, TaskType = ServiceType.ANIMATOR, Request = r1 };
            var t2 = new Task { Name = "Task2", Description = "Neki opis", TaskStatus = TaskStatus.IN_PROGRESS, TaskType = ServiceType.LOCATION, Request = r1 };
            var t3 = new Task { Name = "Task3", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque nec viverra nunc, vel scelerisque eros. Vestibulum varius dignissim nibh, ut suscipit lacus aliquam eget. Cras pulvinar tempus diam, id tincidunt justo. Nulla in tincidunt mi. Aenean hendrerit feugiat ligula. Integer sit amet elementum enim. Mauris ac dictum felis. Morbi feugiat rhoncus quam nec finibus. Integer laoreet urna ac sagittis fringilla. Cras sit amet lorem et ante malesuada volutpat eu ut lacus. Vivamus vel diam dignissim, vulputate lorem at, posuere risus. In vel erat nec risus auctor ullamcorper vel quis tellus. Fusce eget dui bibendum, varius mi ut, sodales enim. Duis accumsan nibh risus, sit amet iaculis lectus malesuada in. Etiam ac ante ligula. Proin et pharetra dolor. ", TaskStatus = TaskStatus.SENT_TO_CLIENT, TaskType = ServiceType.MUSIC, Request = r1 };
            var t4 = new Task { Name = "Task4", Description = "Neki opis", TaskStatus = TaskStatus.REJECTED, TaskType = ServiceType.CATERING, Request = r1 };
            var t5 = new Task { Name = "Task5", Description = "Neki opis", TaskStatus = TaskStatus.TO_DO, TaskType = ServiceType.MUSIC, Request = r1 };
            var t6 = new Task { Name = "Task6", Description = "Neki opis", TaskStatus = TaskStatus.IN_PROGRESS, TaskType = ServiceType.MUSIC, Request = r1 };
            var t7 = new Task { Name = "Task7", Description = "Neki opis", TaskStatus = TaskStatus.TO_DO, TaskType = ServiceType.CATERING, Request = r1 };
            var t8 = new Task { Name = "Task8", Description = "Neki opis", TaskStatus = TaskStatus.TO_DO, TaskType = ServiceType.PHOTOGRAPHY, Request = r1 };
            var t9 = new Task { Name = "Task9", Description = "Neki opis", TaskStatus = TaskStatus.IN_PROGRESS, TaskType = ServiceType.MUSIC, Request = r1 };
            var t10 = new Task { Name = "Task10", Description = "Neki opis", TaskStatus = TaskStatus.TO_DO, TaskType = ServiceType.ANIMATOR, Request = r1 };
            var t11 = new Task { Name = "Task11", Description = "Neki opis", TaskStatus = TaskStatus.TO_DO, TaskType = ServiceType.MUSIC, Request = r1 };
            var t12 = new Task { Name = "Task12", Description = "Neki opis", TaskStatus = TaskStatus.TO_DO, TaskType = ServiceType.CATERING, Request = r1 };
            var t13 = new Task { Name = "Task13", Description = "Neki opis", TaskStatus = TaskStatus.IN_PROGRESS, TaskType = ServiceType.PHOTOGRAPHY, Request = r1 };
            var t14 = new Task { Name = "Task14", Description = "Neki opis", TaskStatus = TaskStatus.ACCEPTED, TaskType = ServiceType.PHOTOGRAPHY, Request = r1 };
            var t15 = new Task { Name = "Task15", Description = "Neki opis", TaskStatus = TaskStatus.SENT_TO_CLIENT, TaskType = ServiceType.MUSIC, Request = r1 };
            var t16 = new Task { Name = "Task16", Description = "Neki opis", TaskStatus = TaskStatus.IN_PROGRESS, TaskType = ServiceType.CATERING, Request = r1 };
            var t17 = new Task { Name = "Task17", Description = "Neki opis", TaskStatus = TaskStatus.REJECTED, TaskType = ServiceType.ANIMATOR, Request = r1 };
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

            var com1 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 1", Sender = c1, Task = t3 };
            var com2 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:20", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 2", Sender = c1, Task = t3 };
            var com3 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:50", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 3", Sender = ep1, Task = t3 };
            var com4 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:51", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 4", Sender = ep1, Task = t3 };
            var com5 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:52", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 5", Sender = ep1, Task = t3 };
            var com6 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:53", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 6", Sender = ep1, Task = t1 };
            var com7 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:54", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 7", Sender = ep1, Task = t1 };
            var com8 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:55", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 8", Sender = ep1, Task = t1 };
            var com9 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:56", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 9", Sender = ep1, Task = t1 };
            var com10 = new Comment { SentDate = DateTime.ParseExact("2021-04-04 14:57", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Content = "comment 10", Sender = ep1, Task = t1 };

            context.Partner.Add(p1);
            context.Partner.Add(p2);

            var o1 = new Offer { Name = "Ponuda 1", Price = 1000, Description = "opis ponude", /*Image = "slika"*/ OfferType = t3.TaskType, Partner = p1 };
            var o2 = new Offer { Name = "Ponuda 2", Price = 2000, Description = "opis ponude 2", /*Image = "slika"*/ OfferType = t3.TaskType, Partner = p2 };
            var o3 = new Offer { Name = "Ponuda 3", Price = 3000, Description = "opis ponude 3", /*Image = "slika"*/ OfferType = t3.TaskType, Partner = par3 };

            context.Offers.Add(o1);
            context.Offers.Add(o2);

            var taskOffer1 = new TaskOffer { Offer = o1, Task = t3, OfferStatus =  OfferStatus.PENDING};
            var taskOffer2 = new TaskOffer { Offer = o2, Task = t3, OfferStatus = OfferStatus.PENDING };
            var taskOffer3 = new TaskOffer { Offer = o3, Task = t3, OfferStatus = OfferStatus.PENDING };
            context.TaskOffers.Add(taskOffer1);
            context.TaskOffers.Add(taskOffer2);
            context.TaskOffers.Add(taskOffer3);

            context.Comments.Add(com1);
            context.Comments.Add(com2);
            context.Comments.Add(com3);
            context.Comments.Add(com4);
            context.Comments.Add(com5);
            context.Comments.Add(com6);
            context.Comments.Add(com7);
            context.Comments.Add(com8);
            context.Comments.Add(com9);
            context.Comments.Add(com10);
            context.Tasks.Add(t11);
            context.Tasks.Add(t12);
            context.Tasks.Add(t13);
            context.Tasks.Add(t14);
            context.Tasks.Add(t15);
            context.Tasks.Add(t16);
            context.Tasks.Add(t17);

            var admin = new Admin { FirstName = "Vidoje", LastName = "Gavrilovic", DateOfBirth = DateTime.Now, Username = "vidojegavrilovic", Password = "test123" };
            context.Admins.Add(admin);

            context.SaveChanges();
        }
    }
}
