using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ISeatingLayoutService
    {
        SeatingLayout Create(SeatingLayout seatingLayout);

        Table AddTable(Table table, int seatingLayoutId);

        void RemoveTable(Table table, int seatingLayoutId);

        Chair AddChair(Chair chair, int  tableId);

        void RemoveChair(Chair chair, int tableId);

        void UpdateTable(Table table);

        void UpdateChair(Chair chair);
    }
}
