using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class RequestDetailsViewModel : ViewModelBase
    {
        private readonly IRequestService _requestService;

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




        public RequestDetailsViewModel(IApplicationContext context, IRequestService requestService) : base(context)
        {
            _requestService = requestService;
            _request = _requestService.GetRequest(1);
        }

    }
}
