using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination.Requests
{
    public class PartnersPage : PageRequest
    {
        public string SearchQuery { get; set; }
    }
}
