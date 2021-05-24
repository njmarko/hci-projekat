using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventPlanner : RequestParticipant
    {
        public virtual IList<Request> AcceptedRequests { get; set; }
    }
}
