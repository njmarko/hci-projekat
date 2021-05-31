using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DuplicateTaskException : Exception
    {
        public DuplicateTaskException(ServiceType type) : base($"Not rejected task with service type {type} already defined for this request.")
        {
        }
    }
}
