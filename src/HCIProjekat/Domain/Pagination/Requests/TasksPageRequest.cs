using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Pagination.Requests
{
    public class TasksPageRequest : PageRequest
    {
        public string Query { get; set; }

        public ServiceType? Type { get; set; }
    
        public TaskStatus? Status { get; set; }
    }
}
