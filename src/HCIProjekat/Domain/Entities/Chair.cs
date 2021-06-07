using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chair : BaseEntity, ILayoutItem
    {
        public string Label { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
    }
}
