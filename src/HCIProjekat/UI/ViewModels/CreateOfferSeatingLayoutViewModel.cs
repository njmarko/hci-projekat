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
