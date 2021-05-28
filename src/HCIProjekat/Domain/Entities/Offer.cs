using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Offer : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public ServiceType OfferType { get; set; }

        public virtual Partner Partner { get; set; }
    }
}
