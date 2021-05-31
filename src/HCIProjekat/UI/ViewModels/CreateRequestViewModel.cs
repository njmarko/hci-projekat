using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class CreateRequestViewModel : ViewModelBase, ISelfValidatingViewModel
    {
        // Poperties
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private RequestTypeModel _requestType;
        public RequestTypeModel RequestTypeValue
        {
            get { return _requestType; }
            set { _requestType = value; OnPropertyChanged(nameof(RequestTypeValue)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private int _guestNumber = 0;
        public int GuestNumber
        {
            get { return _guestNumber; }
            set { _guestNumber = value; OnPropertyChanged(nameof(GuestNumber)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private string _theme = string.Empty;
        public string Theme
        {
            get { return _theme; }
            set { _theme = value; OnPropertyChanged(nameof(Theme)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private int _budget = 0;
        public int Budget
        {
            get { return _budget; }
            set { _budget = value; OnPropertyChanged(nameof(Budget)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private bool _budgetFlexible = true;
        public bool BudgetFlexible
        {
            get { return _budgetFlexible; }
            set { _budgetFlexible = value; OnPropertyChanged(nameof(BudgetFlexible)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private DateTime _requestDate = DateTime.Today;
        public DateTime RequestDate
        {
            get { return _requestDate; }
            set { _requestDate = value; OnPropertyChanged(nameof(RequestDate)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private string _notes = string.Empty;
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(nameof(Notes)); }
        }

        public string ButtonText { get; private set; }

        // Error message view models
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel GuestNumberError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel ThemeError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel BudgetError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel RequestDateError { get; private set; } = new ErrorMessageViewModel();

        public ObservableCollection<RequestTypeModel> RequestTypeModels { get; private set; } = new ObservableCollection<RequestTypeModel>();

        public bool CanCreateRequest => IsValid();
        public ICommand CreateRequest { get; private set; }

        public CreateRequestViewModel(IApplicationContext context, IRequestService requestService, int requestId = -1) : base(context)
        {
            CreateRequest = new CreateRequestCommand(this, requestService, context, requestId);

            RequestTypeValue = new RequestTypeModel { Type = RequestType.BIRTHDAY, Name = "Birthday" };
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.ANNIVERSARY, Name = "Anniversary" });
            RequestTypeModels.Add(RequestTypeValue);
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.GRADUATION, Name = "Graduation" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.PARTY, Name = "Party" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.WEDDING, Name = "Wedding" });

            ButtonText = requestId == -1 ? "Create" : "Save";

            if (requestId != -1)
            {
                Request request = requestService.GetRequest(requestId);
                Name = request.Name;
                Theme = request.Theme;
                GuestNumber = request.GuestNumber;
                BudgetFlexible = request.BudgetFlexible;
                Budget = request.Budget;
                RequestDate = request.Date;
                Notes = request.Notes;
                RequestTypeValue = RequestTypeModels.First(t => t.Type != null && t.Type == request.Type);
            }
        }

        public bool IsValid()
        {
            bool valid = true;

            //Name
            if (string.IsNullOrEmpty(Name))
            {
                NameError.ErrorMessage = "Name is required.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            //Theme
            if (string.IsNullOrEmpty(Theme))
            {
                ThemeError.ErrorMessage = "Theme is required.";
                valid = false;
            }
            else
            {
                ThemeError.ErrorMessage = null;
            }
            //Guest number
            if (GuestNumber <= 0)
            {
                GuestNumberError.ErrorMessage = "Guest number must be a positive number.";
                valid = false;
            } 
            else
            {
                GuestNumberError.ErrorMessage = null;
            }
            //Budget
            if (Budget <= 0)
            {
                BudgetError.ErrorMessage = "Budget must be a positive number.";
                valid = false;
            } 
            else
            {
                BudgetError.ErrorMessage = null;
            }
            //Request date
            if (RequestDate <= DateTime.Today)
            {
                RequestDateError.ErrorMessage = "Request date must be in the future.";
                valid = false;
            }
            else
            {
                RequestDateError.ErrorMessage = null;
            }
            return valid;
        }
    }
}
