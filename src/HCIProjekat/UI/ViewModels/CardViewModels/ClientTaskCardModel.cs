using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.Context;

namespace UI.ViewModels.CardViewModels
{
    public class ClientTaskCardModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ServiceType Type { get; set; }

        public string Status { get; set; }

        public TaskStatus TaskStatus { get; set; }

        public string Color { get; set; }


        public bool IsPending { get; set; }

        public IApplicationContext Context { get; set; }

        public string Route { get; set; }

    }
}
