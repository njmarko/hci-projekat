using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination.Requests
{
    public class OffersPage : PageRequest
    {
        public string OfferName { get; set; }
    }
}
