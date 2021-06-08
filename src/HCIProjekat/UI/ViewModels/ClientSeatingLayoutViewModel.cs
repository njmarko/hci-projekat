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
    public class ClientSeatingLayoutViewModel : ViewModelBase
    {
        private readonly IRequestService _requestService;
        private readonly int _requestId;

        private readonly Request _request;
        private readonly SeatingLayout _seatingLayout;

        public ClientSeatingLayoutViewModel(IApplicationContext context, IRequestService requestService, int requestId) : base(context)
        {
            _requestService = requestService;
            _requestId = requestId;

            _request = _requestService.GetWithGuests(_requestId);
            _seatingLayout = _requestService.GetSeatingLayout(_requestId);
        }
    }
}
