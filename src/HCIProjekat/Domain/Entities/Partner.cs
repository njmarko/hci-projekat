using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Partner : BaseEntity
    {
        public string Name { get; set; }
        public PartnerType Type { get; set; }
        public Location Location { get; set; }

        public virtual IList<Offer> Offers { get; set; }
    }
}
