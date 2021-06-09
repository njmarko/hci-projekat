using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public int RequestId { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public int TaskId { get; set; } = -1;
        public bool Seen { get; set; } = false;
        public bool Read { get; set; } = false;
    }
}
