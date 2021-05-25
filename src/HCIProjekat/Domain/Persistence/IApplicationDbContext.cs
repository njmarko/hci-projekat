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
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<EventPlanner> EventPlanners { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TaskOffer> TaskOffers { get; set; }

        int SaveChanges();
    }
}
