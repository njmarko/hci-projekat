using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class PartnerAlreadyExistsException : Exception
    {
        public PartnerAlreadyExistsException(string name) : base($"Partner {name} already exists.")
        {
        }
    }
}
