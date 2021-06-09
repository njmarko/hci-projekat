using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Util
{
    public class TaskDetails
    {
        public string Status { get; set; }
        public string Color { get; set; }
    }

    public static class TaskColorAndStatus
    {
        public static TaskDetails GetTaskColorAndStatus(Task task)
        {
            switch (task.TaskStatus)
            {
                case TaskStatus.SENT_TO_CLIENT:
                    return new TaskDetails
                    {
                        Color = "#fcc428",
                        Status = "Pending"
                    };
                case TaskStatus.REJECTED:
                    return new TaskDetails
                    {
                        Color = "#de1212",
                        Status = "Rejected"
                    };
                case TaskStatus.TO_DO:
                    return new TaskDetails
                    {
                        Color = "#0d62ff",
                        Status = "To do"
                    };
                case TaskStatus.IN_PROGRESS:
                    return new TaskDetails
                    {
                        Color = "#0d62ff",
                        Status = "In progress"
                    }; ;
                case TaskStatus.ACCEPTED:
                    return new TaskDetails
                    {
                        Color = "#088a35",
                        Status = "Accepted"
                    };
                default:
                    return null;
            }
        }
    }
}
