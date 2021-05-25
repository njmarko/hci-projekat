using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskOffer : BaseEntity
    {
        public OfferStatus OfferStatus { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
