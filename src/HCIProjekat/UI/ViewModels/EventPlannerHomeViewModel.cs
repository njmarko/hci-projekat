using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UI.Context;

namespace UI.ViewModels
{
    public class EventPlannerHomeViewModel : ViewModelBase
    {
        private readonly IEventPlannersService _eventPlannersService;
        private readonly ITaskService _taskService;

        public ObservableCollection<Request> ActiveRequests { get; private set; }

        private Request _currentRequest;
        public Request CurrentRequest
        {
            get { return _currentRequest; }
            set
            {
                if (_currentRequest != value)
                {
                    _currentRequest = value;
                    FetchTasksForSelectedRequest();
                }
            }
        }

        public EventPlannerHomeViewModel(IApplicationContext context, IEventPlannersService eventPlannersService, ITaskService taskService) : base(context)
        {
            _eventPlannersService = eventPlannersService;
            _taskService = taskService;
            ActiveRequests = new ObservableCollection<Request>(_eventPlannersService.GetActiveRequests(11));
            if (ActiveRequests.Count > 0)
            {
                CurrentRequest = ActiveRequests[0];
            }
        }

        private void FetchTasksForSelectedRequest()
        {
            var tasks = _taskService.GetTasksForRequest(_currentRequest.Id, "");
            Console.WriteLine(tasks);
        }
    }
}
