using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination.Requests
{
    public class RequestsPage : PageRequest
    {
        public string Query { get; set; }
        public RequestType? Type { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
