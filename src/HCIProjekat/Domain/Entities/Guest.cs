using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Guest : BaseEntity
    {
        public string Name { get; set; }

        public int ChairId { get; set; }
        public virtual Chair Chair { get; set; }
    }
}
