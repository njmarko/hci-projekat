using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
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

        public Chair AddChair(Chair chair)
        {
            throw new NotImplementedException();
        }

        public Table AddTable(Table table)
        {
            throw new NotImplementedException();
        }

        public SeatingLayout Create(SeatingLayout seatingLayout)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.SeatingLayouts.Add(seatingLayout);
            context.SaveChanges();

            return seatingLayout;
        }
    }
}
