using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination.Requests
{
    public class AdminsPage : PageRequest
    {
        public string Query { get; set; }
        public DateTime? BornBefore { get; set; }
        public DateTime? BornAfter { get; set; }
    }
}
