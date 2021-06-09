using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class CommentViewModel
    {
        public DateTime SentDate { get; set; }
        public string Content { get; set; }

        public string Sender { get; set; }

        public string Color { get; set; }

        public string Margin { get; set; }

    }
}
