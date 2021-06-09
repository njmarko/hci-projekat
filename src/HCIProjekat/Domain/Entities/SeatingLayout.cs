using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SeatingLayout : BaseEntity
    {
        public int OfferId { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual IList<Table> Tables { get; set; } = new List<Table>();
    }
}
