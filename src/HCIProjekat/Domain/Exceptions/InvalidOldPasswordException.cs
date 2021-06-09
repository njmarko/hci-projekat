using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidOldPasswordException : Exception
    {
        public InvalidOldPasswordException(string message) : base("Invalid old password.")
        {
        }
    }
}
