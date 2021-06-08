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
using UI.CustomAttributes;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class CreateRequestViewModel : ValidationModel<CreateRequestViewModel>
    {
        // Poperties
        private string _name = string.Empty;
        [ValidationField]
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

        private string _guestNumber = "0";
        [ValidationField]
        public string GuestNumber
        {
            get { return _guestNumber; }
            set { _guestNumber = value; OnPropertyChanged(nameof(GuestNumber)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private string _theme = string.Empty;
        [ValidationField]
        public string Theme
        {
            get { return _theme; }
            set { _theme = value; OnPropertyChanged(nameof(Theme)); OnPropertyChanged(nameof(CanCreateRequest)); }
        }

        private string _budget = "0";
        [ValidationField]
        public string Budget
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


        private string _title;

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _help;

        public string Help
        {
            get 
            { 
                return _help; 
            }
            set 
            { 
                _help = value;
                OnPropertyChanged(nameof(Help));
            }
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

        public CreateRequestViewModel(ClientRequestsViewModel vm, IApplicationContext context, IRequestService requestService, int requestId = -1) : base(context)
        {
            CreateRequest = new CreateRequestCommand(this, vm, requestService, context, requestId);

            RequestTypeValue = new RequestTypeModel { Type = RequestType.BIRTHDAY, Name = "Birthday" };
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.ANNIVERSARY, Name = "Anniversary" });
            RequestTypeModels.Add(RequestTypeValue);
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.GRADUATION, Name = "Graduation" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.PARTY, Name = "Party" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.WEDDING, Name = "Wedding" });

            ButtonText = requestId == -1 ? "Create" : "Save";
            Title = "Create request";
            Help = "create-request";

            if (requestId != -1)
            {
                Request request = requestService.Get(requestId);
                Name = request.Name;
                Theme = request.Theme;
                GuestNumber = request.GuestNumber.ToString();
                BudgetFlexible = request.BudgetFlexible;
                Budget = request.Budget.ToString();
                RequestDate = request.Date;
                Notes = request.Notes;
                RequestTypeValue = RequestTypeModels.First(t => t.Type != null && t.Type == request.Type);
                Title = $"Edit {Name}";
                Help = "edit-requests";
            }
        }

        public bool IsValid()
        {
            bool valid = true;

            //Name
            if (string.IsNullOrEmpty(Name) && IsDirty(nameof(Name)))
            {
                NameError.ErrorMessage = "Name is required.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            //Theme
            if (string.IsNullOrEmpty(Theme) && IsDirty(nameof(Theme)))
            {
                ThemeError.ErrorMessage = "Theme is required.";
                valid = false;
            }
            else
            {
                ThemeError.ErrorMessage = null;
            }


            //Guest number
            if (string.IsNullOrEmpty(GuestNumber) && IsDirty(nameof(GuestNumber)))
            {
                GuestNumberError.ErrorMessage = "Guest number is required!";
                valid = false;
            }
            else if (int.TryParse(GuestNumber, out int guestNum))
            {
                if (guestNum <= 0 && IsDirty(nameof(GuestNumber)))
                {
                    GuestNumberError.ErrorMessage = "Guest number must be greater than 0!";
                    valid = false;
                }
                else
                {
                    GuestNumberError.ErrorMessage = null;
                }
            }
            else if (IsDirty(nameof(GuestNumber)))
            {
                GuestNumberError.ErrorMessage = "Guest number must be number!";
                valid = false;
            }



            //Budget
            if (string.IsNullOrEmpty(Budget) && IsDirty(nameof(Budget)))
            {
                BudgetError.ErrorMessage = "Budget is required!";
                valid = false;
            }
            else if (int.TryParse(Budget, out int budgetNumber))
            {
                if (budgetNumber <= 0 && IsDirty(nameof(Budget)))
                {
                    BudgetError.ErrorMessage = "Budget number must be greater than 0!";
                    valid = false;
                }
                else
                {
                    BudgetError.ErrorMessage = null;
                }
            }
            else if (IsDirty(nameof(Budget)))
            {
                BudgetError.ErrorMessage = "Budget must be number!";
                valid = false;
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
            return valid && AllDirty();
        }
    }
}
