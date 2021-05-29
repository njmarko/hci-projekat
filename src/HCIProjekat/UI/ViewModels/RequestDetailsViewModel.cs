using Domain.Entities;
using Domain.Pagination.Requests;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;
using UI.ViewModels.CardViewModels;

namespace UI.ViewModels
{
    public class RequestDetailsViewModel : PagingViewModelBase
    {
        private readonly IRequestService _requestService;
        private readonly ITaskService _taskService;

        private Request _request;

        public Request Request
        {
            get { return _request; }
            set 
            { 
                _request = value;
                OnPropertyChanged(nameof(Request));
            }
        }

        private string _taskName;

        public string TaskName
        {
            get { return _taskName; }
            set 
            { 
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }



        public ObservableCollection<ClientTaskCardModel> TaskModels { get; private set; } = new ObservableCollection<ClientTaskCardModel>();




        public RequestDetailsViewModel(IApplicationContext context, IRequestService requestService, ITaskService taskService) : base(context)
        {
            Rows = 2;
            Columns = 4;
            TaskName = "";
            _requestService = requestService;
            _taskService = taskService;
            _request = _requestService.GetRequest(1);
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            TaskModels.Clear();
            var page = _taskService.GetTasksForRequest(_request.Id, new TasksPageRequest { Size = Size, Page = pageNumber, TaskName = TaskName});
            foreach (var entity in page.Entities)
            {
                TaskModels.Add(new ClientTaskCardModel { Name = entity.Name,Description = entity.Description });
            }
            OnPageFetched(page);
        }
    }
}
