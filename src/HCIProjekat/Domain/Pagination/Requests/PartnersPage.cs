using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination.Requests
{
    public class PartnersPage : PageRequest
    {
        public string Query { get; set; }

        public PartnerType? PartnerType { get; set; }
    }
}
