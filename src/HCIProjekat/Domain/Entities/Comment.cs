using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public DateTime SentDate { get; set; }
        public string Content { get; set; }
        public virtual RequestParticipant Sender { get; set; }
        public virtual Task Task { get; set; }
    }
}
