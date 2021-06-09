﻿using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static byte[] ReadFromFile(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = File.ReadAllBytes(fileName);
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                return bytes;
            }
        }

        public static void Seed(ApplicationDbContext context)
        {
            var c1 = new Client { FirstName = "Dejan", LastName = "Djordjevic", Username = "dejandjordjevic", Password = "test123", DateOfBirth = DateTime.ParseExact("1999-04-04 14:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };
            var c2 = new Client { FirstName = "Borivoje", LastName = "Vladić", Username = "borivojevladic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-20) };
            var c3 = new Client { FirstName = "Darinka", LastName = " Ignjatović", Username = "darinkaignjatovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-23) };
            var c4 = new Client { FirstName = "Vlatka", LastName = "Gavrilović", Username = "vlatkagavrilovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-33) };
            var c5 = new Client { FirstName = "Vujica", LastName = "Moldovan", Username = "vujicamoldovan", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-43) };
            var c6 = new Client { FirstName = "Vladana", LastName = "Trkulja", Username = "vladanatrkulja", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-53) };
            var c7 = new Client { FirstName = "Slavomir", LastName = "Trkulja", Username = "slavomirtrkulja", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-63) };
            var c8 = new Client { FirstName = "Desanka", LastName = "Vukašinović", Username = "desankavukasinovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-73) };
            var c9 = new Client { FirstName = "Sredoje", LastName = "Gojković", Username = "sredojegojkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-83) };
            var c10 = new Client { FirstName = "Petra", LastName = "Kuzmanović", Username = "petrakuzmanovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-91) };
            var c11 = new Client { FirstName = "Lada", LastName = "Borisov", Username = "ladaborisov", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-28) };
            var c12 = new Client { FirstName = "Dobrila", LastName = "Jovanović", Username = "dobrilajovanovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-55) };
            var c13 = new Client { FirstName = "Dalbor", LastName = "Malić", Username = "dalbormalic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-44) };
            var c14 = new Client { FirstName = "Cvetko", LastName = "Brkić", Username = "cvetkobrkic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-22) };
            var c15 = new Client { FirstName = "Bojan", LastName = "Grgurović", Username = "bojangrgurovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-31) };
            var c16 = new Client { FirstName = "Zeljana", LastName = "Bojanić", Username = "zeljanabojanic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-21) };
            var c17 = new Client { FirstName = "Leposava", LastName = "Živić", Username = "petrakuzmanovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-41) };
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
            context.Clients.Add(c11);
            context.Clients.Add(c12);
            context.Clients.Add(c13);
            context.Clients.Add(c14);
            context.Clients.Add(c15);
            context.Clients.Add(c16);
            context.Clients.Add(c17);

            var r5 = new Request { Name = "Diplomiranje Dalbora Malića", Budget = 200000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(10), GuestNumber = 20, Type = RequestType.GRADUATION, Theme = "Alkohol", Notes = "Veliki dan za studente Republike Srpske i prijatelje." };
            var r2 = new Request { Name = "Udaja Lade Brđanin", Budget = 300000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(12), GuestNumber = 40, Type = RequestType.WEDDING, Theme = "Etno", Notes = "Voditi računa da se poštuje tradicija." };
            var r3 = new Request { Name = "Doček Nikole Jokića", Budget = 860000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(13), GuestNumber = 1000, Type = RequestType.PARTY, Theme = "MVP", Notes = "Dočekati kako dolikuje velikog šampiona." };
            var r4 = new Request { Name = "Rođendan Zorane Čarapić", Budget = 270000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(14), GuestNumber = 20, Type = RequestType.BIRTHDAY, Theme = "Ex Yu Nostalgija", Notes = "Što više patetike to bolje." };
            var r1 = new Request { Name = "Godišnjica ispita iz analize", Budget = 380000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(25), GuestNumber = 8, Type = RequestType.ANNIVERSARY, Theme = "Nostalgija", Notes = "Sinus voli minus." };
            var r6 = new Request { Name = "Žurka studenata Republike Srpske", Budget = 490000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(16), GuestNumber = 120, Type = RequestType.PARTY, Theme = "Alkohol", Notes = "Što glasnije to bolje." };
            var r7 = new Request { Name = "Ženidba Zdeslava Dapčevića", Budget = 110000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(17), GuestNumber = 42, Type = RequestType.WEDDING, Theme = "Naučna Fantastika", Notes = "Mladoženja Darth Vader, mlada Storm Trooper." };
            var r8 = new Request { Name = "Sabor šabana u Stepojevcu", Budget = 220000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(18), GuestNumber = 332, Type = RequestType.PARTY, Theme = "Šator", Notes = "Obezbediti mesto za parking." };
            var r9 = new Request { Name = "Rođendan Veselinka Borisov", Budget = 330000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(19), GuestNumber = 22, Type = RequestType.BIRTHDAY, Theme = "Barok", Notes = "Nošnja i muzika iz baroknog perioda." };
            var r10 = new Request { Name = "Doček sina sa rehabilitacije iz Crne Reke", Budget = 19000, BudgetFlexible = true, Client = c2, Date = DateTime.Now.AddDays(7), GuestNumber = 6, Type = RequestType.PARTY, Theme = "Porodična", Notes = "Mirna proslava." };
            var r11 = new Request { Name = "Ispraćaj Nedeljka Vignjevića u Vojsku", Budget = 28000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(6), GuestNumber = 12, Type = RequestType.PARTY, Theme = "Vojska", Notes = "Budući tenkista." };
            var r12 = new Request { Name = "Upis ćerke na SIIT", Budget = 37000, BudgetFlexible = true, Client = c1, Date = DateTime.Now.AddDays(-5), GuestNumber = 12, Type = RequestType.PARTY, Theme = "Elitizam", Notes = "Upis na elitni smer softverskog inženjerstva." };
            var r13 = new Request { Name = "Godišnjica DJT pobede", Budget = 46000, BudgetFlexible = true, Client = c4, Date = DateTime.Now.AddDays(-4), GuestNumber = 12, Type = RequestType.ANNIVERSARY, Theme = "Crvena kapa", Notes = "Gosti su zidari." };
            var r14 = new Request { Name = "Godišnjica završetka studija na SIIT", Budget = 55000, BudgetFlexible = true, Client = c5, Date = DateTime.Now.AddDays(13), GuestNumber = 12, Type = RequestType.GRADUATION, Theme = "Alkoloh", Notes = "Prve tri godine su teške, a onda upišeš drugu." };

            var l1 = new Location { StreetNumber = "301", Street = "Ulica1", City = "Novi Sad", Country = "Srbija" };
            var l2 = new Location { StreetNumber = "302", Street = "Ulica2", City = "Novi Sad", Country = "Srbija" };
            var l3 = new Location { StreetNumber = "303", Street = "Ulica3", City = "Novi Sad", Country = "Srbija" };

            var p1 = new Partner { Name = "Partner 1", Location = l1, Type = PartnerType.RESTAURANT };
            var p2 = new Partner { Name = "Partner 2", Location = l2, Type = PartnerType.RESTAURANT };
            var p3 = new Partner { Name = "Partner 3", Location = l3, Type = PartnerType.RESTAURANT };

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

            var ep1 = new EventPlanner { FirstName = "Jakov", LastName = "Matic", Username = "jakovmatic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-38), AcceptedRequests = new List<Request>() };
            var ep2 = new EventPlanner { FirstName = "Damjanka", LastName = "Stevanović", Username = "damjankastevanovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-20), AcceptedRequests = new List<Request>() };
            var ep3 = new EventPlanner { FirstName = "Roksana", LastName = "Vukomanović", Username = "roksanavukomanovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-30), AcceptedRequests = new List<Request>() };
            var ep4 = new EventPlanner { FirstName = "Zeljana", LastName = "Georgijević", Username = "zeljanageorgijevic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-40), AcceptedRequests = new List<Request>() };
            var ep5 = new EventPlanner { FirstName = "Violeta", LastName = "Popadić", Username = "violetapopadic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-50), AcceptedRequests = new List<Request>() };
            var ep6 = new EventPlanner { FirstName = "Stracimir", LastName = "Obradović", Username = "stracimirobradovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-60), AcceptedRequests = new List<Request>() };
            var ep7 = new EventPlanner { FirstName = "Pribislav", LastName = "Moldovan", Username = "pribislavmoldovan", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-70), AcceptedRequests = new List<Request>() };
            var ep8 = new EventPlanner { FirstName = "Obrad", LastName = "Nestorovski", Username = "obradnestorovski", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-80), AcceptedRequests = new List<Request>() };
            var ep9 = new EventPlanner { FirstName = "Anka", LastName = "Brđanin", Username = "ankabrdjanin", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-90), AcceptedRequests = new List<Request>() };
            var ep10 = new EventPlanner { FirstName = "Mića", LastName = "Živac", Username = "e", Password = "e", DateOfBirth = DateTime.Now.AddYears(-23), AcceptedRequests = new List<Request>() };
            var ep11 = new EventPlanner { FirstName = "Radojka", LastName = "Pap", Username = "radojkapap", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-43), AcceptedRequests = new List<Request>() };
            var ep12 = new EventPlanner { FirstName = "Malina", LastName = "Nedeljković", Username = "malinanedeljkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-33), AcceptedRequests = new List<Request>() };
            var ep13 = new EventPlanner { FirstName = "Lješ", LastName = "Filipović", Username = "ljesfilipovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-41), AcceptedRequests = new List<Request>() };
            var ep14 = new EventPlanner { FirstName = "Stanoje", LastName = "Popović", Username = "stanojepopovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-52), AcceptedRequests = new List<Request>() };
            var ep15 = new EventPlanner { FirstName = "Ljubisav", LastName = "Marinković", Username = "ljubislavmarinkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-66), AcceptedRequests = new List<Request>() };
            var ep16 = new EventPlanner { FirstName = "Draginja", LastName = "Grgurović", Username = "draginjagrgurovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-75), AcceptedRequests = new List<Request>() };
            var ep17 = new EventPlanner { FirstName = "Varadinka", LastName = "Despotović", Username = "varadinkadespotovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-34), AcceptedRequests = new List<Request>() };
            ep1.AcceptedRequests.Add(r1);
            ep1.AcceptedRequests.Add(r2);
            ep1.AcceptedRequests.Add(r3);
            ep1.AcceptedRequests.Add(r4);
            ep2.AcceptedRequests.Add(r5);
            ep2.AcceptedRequests.Add(r6);
            ep2.AcceptedRequests.Add(r7);
            ep3.AcceptedRequests.Add(r8);
            ep1.AcceptedRequests.Add(r10);
            ep1.AcceptedRequests.Add(r11);
            ep1.AcceptedRequests.Add(r12);
            ep2.AcceptedRequests.Add(r13);
            ep3.AcceptedRequests.Add(r14);

            context.EventPlanners.Add(ep1);
            context.EventPlanners.Add(ep2);
            context.EventPlanners.Add(ep3);
            context.EventPlanners.Add(ep4);
            context.EventPlanners.Add(ep5);
            context.EventPlanners.Add(ep6);
            context.EventPlanners.Add(ep7);
            context.EventPlanners.Add(ep8);
            context.EventPlanners.Add(ep9);
            context.EventPlanners.Add(ep10);
            context.EventPlanners.Add(ep11);
            context.EventPlanners.Add(ep12);
            context.EventPlanners.Add(ep13);
            context.EventPlanners.Add(ep14);
            context.EventPlanners.Add(ep15);
            context.EventPlanners.Add(ep16);
            context.EventPlanners.Add(ep17);

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

            context.Partners.Add(par1);
            context.Partners.Add(par2);
            context.Partners.Add(par3);
            context.Partners.Add(par4);
            context.Partners.Add(par5);
            context.Partners.Add(par6);
            context.Partners.Add(par7);
            context.Partners.Add(par8);
            context.Partners.Add(par9);
            context.Partners.Add(par10);

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

            context.Partners.Add(p1);
            context.Partners.Add(p2);
            context.Partners.Add(p3);

            var o1 = new Offer { Name = "Ponuda 1", Price = 1000, Description = "opis ponude", /*Image = ""*/ OfferType = t3.TaskType, Partner = p1 };
            var o2 = new Offer { Name = "Ponuda 2", Price = 2000, Description = "opis ponude 2", /*Image = "slika"*/ OfferType = t3.TaskType, Partner = p2 };
            var o3 = new Offer { Name = "Ponuda 3", Price = 3000, Description = "opis ponude 3", /*Image = "slika"*/ OfferType = t3.TaskType, Partner = par3 };

            context.Offers.Add(o1);
            context.Offers.Add(o2);

            var taskOffer1 = new TaskOffer { Offer = o1, Task = t3, OfferStatus = OfferStatus.PENDING};
            var taskOffer2 = new TaskOffer { Offer = o2, Task = t3, OfferStatus = OfferStatus.PENDING };
            var taskOffer3 = new TaskOffer { Offer = o3, Task = t3, OfferStatus = OfferStatus.PENDING };
            var taskOffer4 = new TaskOffer { Offer = o1, Task = t3, OfferStatus = OfferStatus.PENDING };
            context.TaskOffers.Add(taskOffer1);
            context.TaskOffers.Add(taskOffer2);
            context.TaskOffers.Add(taskOffer3);
            context.TaskOffers.Add(taskOffer4);

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

            var admin1 = new Admin { FirstName = "Vidoje", LastName = "Gavrilovic", DateOfBirth = DateTime.Now.AddYears(-45), Username = "vidojegavrilovic", Password = "test123" };
            var admin2 = new SuperAdmin { FirstName = "Djordje", LastName = "Vrući", DateOfBirth = DateTime.Now.AddYears(-30), Username = "a", Password = "a" };
            var admin3 = new Admin { FirstName = "Damjanka", LastName = "Gojković", DateOfBirth = DateTime.Now.AddYears(-22), Username = "damjankagojkovic", Password = "test123" };
            var admin4 = new Admin { FirstName = "Simonida", LastName = "Čarapić", DateOfBirth = DateTime.Now.AddYears(-31), Username = "simonidacarapic", Password = "test123" };
            var admin5 = new Admin { FirstName = "Borka", LastName = "Mijatović", DateOfBirth = DateTime.Now.AddYears(-40), Username = "borkamijatovic", Password = "test123" };
            var admin6 = new Admin { FirstName = "Vidoje", LastName = "Šaponjić", DateOfBirth = DateTime.Now.AddYears(-55), Username = "vidojesaponjic", Password = "test123" };
            var admin7 = new Admin { FirstName = "Vukajlo", LastName = "Nikolić", DateOfBirth = DateTime.Now.AddYears(-67), Username = "vukajlonikolic", Password = "test123" };
            var admin8 = new Admin { FirstName = "Savatije", LastName = "Ristić", DateOfBirth = DateTime.Now.AddYears(-48), Username = "savatijeristic", Password = "test123" };
            var admin9 = new Admin { FirstName = "Daliborka", LastName = "Pavlović", DateOfBirth = DateTime.Now.AddYears(-39), Username = "daliborkapavlovic", Password = "test123" };
            context.Admins.Add(admin1);
            context.SuperAdmins.Add(admin2);
            context.Admins.Add(admin3);
            context.Admins.Add(admin4);
            context.Admins.Add(admin5);
            context.Admins.Add(admin6);
            context.Admins.Add(admin7);
            context.Admins.Add(admin8);
            context.Admins.Add(admin9);



            // Notifications
            var n1 = new Notification { Message = "Notifikacija 1", UserId = 11, RequestId = 1 };
            var n2 = new Notification { Message = "Notifikacija 2", UserId = 11, RequestId = 1 };
            var n3 = new Notification { Message = "Notifikacija 3", UserId = 11, RequestId = 1 };
            var n4 = new Notification { Message = "Notifikacija 4", UserId = 11, RequestId = 1 };
            var n5 = new Notification { Message = "Notifikacija 5", UserId = 11, RequestId = 1 };
            var n6 = new Notification { Message = "Notifikacija 6", UserId = 11, RequestId = 1 };
            var n7 = new Notification { Message = "Notifikacija 7", UserId = 11, RequestId = 1 };
            var n8 = new Notification { Message = "Notifikacija 8", UserId = 11, RequestId = 1 };
            var n9 = new Notification { Message = "Notifikacija 9", UserId = 11, RequestId = 1 };
            var n10 = new Notification { Message = "Notifikacija 10", UserId = 11, RequestId = 1 };
            context.Notifications.Add(n1);
            context.Notifications.Add(n2);
            context.Notifications.Add(n3);
            context.Notifications.Add(n4);
            context.Notifications.Add(n5);
            context.Notifications.Add(n6);
            context.Notifications.Add(n7);
            context.Notifications.Add(n8);
            context.Notifications.Add(n9);
            context.Notifications.Add(n10);

            context.SaveChanges();
        }
    }
}
