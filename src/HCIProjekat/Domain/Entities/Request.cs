using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Request : BaseEntity
    {
        public string Name { get; set; }
        public RequestType Type { get; set; }
        public int GuestNumber { get; set; }
        public string Theme { get; set; }
        public int Budget { get; set; }
        public bool BudgetFlexible { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public Client Client { get; set; }
        public EventPlanner EventPlanner { get; set; }
        public virtual IList<Task> Tasks { get; set; }
        public virtual IList<Guest> Guests { get; set; }
    }
}
