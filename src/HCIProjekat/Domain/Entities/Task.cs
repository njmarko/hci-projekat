using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Task : BaseEntity
    {
        public TaskStatus TaskStatus { get; set; }
        public ServiceType TaskType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Request Request { get; set; }
        public virtual IList<TaskOffer> Offers { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}
