using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination.Requests
{
    public class EventPlannerRequestsPage : RequestsPage
    {
        public bool Mine { get; set; }
        public bool OnlyNew { get; set; }
    }
}
