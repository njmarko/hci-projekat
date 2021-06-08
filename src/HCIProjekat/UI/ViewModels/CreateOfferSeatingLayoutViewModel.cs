using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;

namespace UI.ViewModels
{
    public class CreateOfferSeatingLayoutViewModel : ViewModelBase
    {
        private readonly CreateOfferViewModel _createOfferVm;
        private readonly ISeatingLayoutService _seatingLayoutService;
        
        public SeatingLayout SeatingLayout { get; private set; }

        public ICommand SaveChanges { get; private set; }

        public CreateOfferSeatingLayoutViewModel(CreateOfferViewModel createOfferVm, IApplicationContext context, ISeatingLayoutService seatingLayoutService, SeatingLayout seatingLayout) : base(context)
        {
            _createOfferVm = createOfferVm;
            _seatingLayoutService = seatingLayoutService;
            SeatingLayout = seatingLayout;

            SaveChanges = new DelegateCommand(SaveChangesImpl);
        }

        private void SaveChangesImpl()
        {
            if (SeatingLayout.Id != 0)
            {

            }
        }

        public Table ClosestTable(double xOffset, double yOffset)
        {
            return SeatingLayout.Tables.OrderBy(t => Distance(t, xOffset, yOffset)).FirstOrDefault();
        }

        public double Distance(ILayoutItem item, double xOffset, double yOffset)
        {
            double xDiff = item.X - xOffset;
            double yDiff = item.Y - yOffset;
            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        public bool ChairsPresentInTable(double xOffset, double yOffset)
        {
            var table = SeatingLayout.Tables.Where(t => t.X == xOffset && t.Y == yOffset).FirstOrDefault();
            return table.Chairs.Count > 0;
        }

        public void UpdateTable(double xOffset, double yOffset, double newXOffset, double newYOffset)
        {
            var table = SeatingLayout.Tables.Where(t => t.X == xOffset && t.Y == yOffset).FirstOrDefault();
            table.X = newXOffset;
            table.Y = newYOffset;
            if (SeatingLayout.Id != 0)
            {
                _seatingLayoutService.UpdateTable(table);
            }
        }

        public void UpdateChair(double xOffset, double yOffset, double newXOffset, double newYOffset)
        {
            var closestTable = ClosestTable(xOffset, yOffset);
            var newClosestTable = ClosestTable(newXOffset, newYOffset);
            if (closestTable == null)
            {
                throw new Exception("No table present.");
            }

            var chair = closestTable.Chairs.Where(c => c.X == xOffset && c.Y == yOffset).FirstOrDefault();
            chair.X = newXOffset;
            chair.Y = newYOffset;

            if (closestTable == newClosestTable)
            {
                return;
            }

            closestTable.Chairs.Remove(chair);
            newClosestTable.Chairs.Add(chair);

            UpdateChairIndexes(closestTable);
            UpdateChairIndexes(newClosestTable);

            if (SeatingLayout.Id != 0)
            {
                _seatingLayoutService.UpdateChair(chair);
                _seatingLayoutService.UpdateTable(newClosestTable);
            }
        }

        private void UpdateChairIndexes(Table table)
        {
            var startingIndex = 1;
            foreach (Chair chair in table.Chairs)
            {
                chair.Label = $"{startingIndex++}";
                _seatingLayoutService.UpdateChair(chair);
            }
        }

        public void RemoveTable(double xOffset, double yOffset)
        {
            var table = SeatingLayout.Tables.Where(t => t.X == xOffset && t.Y == yOffset).FirstOrDefault();
            SeatingLayout.Tables.Remove(table);
            if (SeatingLayout.Id != 0)
            {
                _seatingLayoutService.RemoveTable(table, SeatingLayout.Id);
            }
        }

        public Table AddTable(double xOffset, double yOffset)
        {
            var tableNum = SeatingLayout.Tables.Count + 1;
            var table = new Table { X = xOffset, Y = yOffset, Radius = 40, Label = $"Table #{tableNum}" };
            SeatingLayout.Tables.Add(table);
            if (SeatingLayout.Id != 0)
            {
                _seatingLayoutService.AddTable(table, SeatingLayout.Id);
            }
            return table;
        }

        public void RemoveChair(double xOffset, double yOffset)
        {
            var closestTable = ClosestTable(xOffset, yOffset);
            if (closestTable == null)
            {
                throw new Exception("No table present.");
            }

            var chair = closestTable.Chairs.Where(c => c.X == xOffset && c.Y == yOffset).FirstOrDefault();
            closestTable.Chairs.Remove(chair);
            if (SeatingLayout.Id != 0)
            {
                _seatingLayoutService.RemoveChair(chair, SeatingLayout.Id);
            }
        }

        public Chair AddChair(double xOffset, double yOffset)
        {
            var closestTable = ClosestTable(xOffset, yOffset);
            if (closestTable == null)
            {
                throw new Exception("No table present.");
            }
            var chairNum = closestTable.Chairs.Count + 1;
            var chair = new Chair { X = xOffset, Y = yOffset, Radius = 10, Label = $"{chairNum}" };
            closestTable.Chairs.Add(chair);
            if (SeatingLayout.Id != 0)
            {
                _seatingLayoutService.AddChair(chair, closestTable.Id);
            }
            return chair;
        }
    }
}
