using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination.Requests
{
    public class TasksPageRequest : PageRequest
    {
        public string TaskName { get; set; }
    }
}
