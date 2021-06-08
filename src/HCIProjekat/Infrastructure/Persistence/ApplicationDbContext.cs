using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
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
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SeatingLayout> SeatingLayouts { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
