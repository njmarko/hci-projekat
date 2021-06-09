using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Table : BaseEntity, ILayoutItem
    {
        public string Label { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }

        public virtual IList<Chair> Chairs { get; set; } = new List<Chair>();
    }
}
