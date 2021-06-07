using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class CreateOfferSeatingLayoutViewModel : ViewModelBase
    {
        private readonly CreateOfferViewModel _createOfferVm;
        
        public SeatingLayout SeatingLayout { get; private set; }

        public CreateOfferSeatingLayoutViewModel(CreateOfferViewModel createOfferVm, IApplicationContext context, SeatingLayout seatingLayout) : base(context)
        {
            _createOfferVm = createOfferVm;
            SeatingLayout = seatingLayout ?? new SeatingLayout { Tables = new List<Table>() };
        }

        public Table ClosestTable(double xOffset, double yOffset)
        {
            return SeatingLayout.Tables.OrderBy(t => Distance(t, xOffset, yOffset)).FirstOrDefault();
        }

        public double Distance(ILayoutItem item, double xOffset, double yOffset)
        {
            double xDiff = item.X - xOffset;
            double yDiff = item.Y - yOffset;
            return Math.Abs(xDiff * xDiff + yDiff * yDiff);
        }
    }
}
