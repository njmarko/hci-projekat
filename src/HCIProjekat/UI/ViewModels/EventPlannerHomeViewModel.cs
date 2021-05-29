using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class EventPlannerHomeViewModel : ViewModelBase
    {
        private readonly IEventPlannersService _eventPlannersService;

        public ObservableCollection<Request> ActiveRequests { get; private set; }
        public Request CurrentRequest { get; set; }

        public EventPlannerHomeViewModel(IApplicationContext context, IEventPlannersService eventPlannersService) : base(context)
        {
            _eventPlannersService = eventPlannersService;
            ActiveRequests = new ObservableCollection<Request>(_eventPlannersService.GetActiveRequests(11));
            if (ActiveRequests.Count > 0)
            {
                CurrentRequest = ActiveRequests[0];
            }
        }
    }
}
