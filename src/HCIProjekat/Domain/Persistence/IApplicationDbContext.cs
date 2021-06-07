using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Persistence
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<EventPlanner> EventPlanners { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<Request> Requests { get; set; }
        DbSet<Partner> Partners { get; set; }
        DbSet<Offer> Offers { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<TaskOffer> TaskOffers { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<SeatingLayout> SeatingLayouts { get; set; }
        DbSet<Table> Tables { get; set; }
        DbSet<Chair> Chairs { get; set; }


        int SaveChanges();
    }
}
