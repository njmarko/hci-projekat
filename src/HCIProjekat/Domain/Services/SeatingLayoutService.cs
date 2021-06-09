using Domain.Entities;
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
    public class SeatingLayoutService : ISeatingLayoutService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;

        public SeatingLayoutService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Chair AddChair(Chair chair, int tableId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var table = context.Tables.Include(t => t.Chairs).SingleOrDefault(t => t.Id == tableId);
            table.Chairs.Add(chair);
            context.SaveChanges();

            return chair;
        }

        public Table AddTable(Table table, int seatingLayoutId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var layout = context.SeatingLayouts.Include(l => l.Tables).SingleOrDefault(l => l.Id == seatingLayoutId);
            layout.Tables.Add(table);
            context.SaveChanges();

            return table;
        }

        public SeatingLayout Create(SeatingLayout seatingLayout)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.SeatingLayouts.Add(seatingLayout);
            context.SaveChanges();

            return seatingLayout;
        }

        public void RemoveChair(Chair chair, int tableId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var table = context.Tables.Include(t => t.Chairs).SingleOrDefault(t => t.Id == tableId);
            table.Chairs.Remove(chair);
            context.SaveChanges();
        }

        public void RemoveTable(Table table, int seatingLayoutId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var layout = context.SeatingLayouts.Include(l => l.Tables).SingleOrDefault(l => l.Id == seatingLayoutId);
            layout.Tables.Remove(table);

            context.SaveChanges();
        }

        public void UpdateChair(Chair chair)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Chairs.Update(chair);
            context.SaveChanges();
        }

        public void UpdateTable(Table table)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Tables.Update(table);
            context.SaveChanges();
        }
    }
}
