using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UI.ViewModels
{
    public class EventPlannerTaskOfferCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
    }

    public class EventPlannerTaskDetailsViewModel : PagingViewModelBase
    {
        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value; }
        }

        public override void UpdatePage(int pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
