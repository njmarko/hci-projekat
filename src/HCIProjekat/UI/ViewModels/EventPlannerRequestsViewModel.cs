using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class EventPlannerRequestsViewModel : PagingViewModelBase
    {
        private readonly IRequestService _requestService;

        public EventPlannerRequestsViewModel(IApplicationContext context, IRequestService requestService) : base(context)
        {
            _requestService = requestService;
        }

        public override void UpdatePage(int pageNumber)
        {
            
        }
    }
}
