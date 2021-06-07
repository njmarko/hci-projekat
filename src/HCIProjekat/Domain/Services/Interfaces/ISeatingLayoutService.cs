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

        Chair AddChair(Chair chair, int  tableId);
    }
}
