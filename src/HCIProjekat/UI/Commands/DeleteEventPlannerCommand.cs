using Domain.Services;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Commands
{
    public class DeleteEventPlannerCommand : ICommand
    {
        private readonly AdminEventPlannersViewModel _adminEventPlannersViewModel;
        private readonly int _eventPlannerId;
        private readonly string _eventPlannerUsername;
        private readonly IModalService _modalService;
        private readonly IEventPlannersService _eventPlannerService;

        public event EventHandler CanExecuteChanged;

        public DeleteEventPlannerCommand(AdminEventPlannersViewModel adminEventPlannersVm, int eventPlannerId, string eventPlannerUsername, IModalService modalService, IEventPlannersService eventPlannerService)
        {
            _adminEventPlannersViewModel = adminEventPlannersVm;
            _eventPlannerId = eventPlannerId;
            _eventPlannerUsername = eventPlannerUsername;
            _modalService = modalService;
            _eventPlannerService = eventPlannerService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_modalService.ShowConfirmationDialog($"Are you sure you want to delete event planner {_eventPlannerUsername}?"))
            {
                // undo redo
                var eventPlanner = _eventPlannerService.Get(_eventPlannerId);
                _adminEventPlannersViewModel.AddItem(eventPlanner);

                _eventPlannerService.Delete(_eventPlannerId);
                _adminEventPlannersViewModel.UpdatePage(0);
                _adminEventPlannersViewModel.Context.Notifier.ShowInformation($"Event planner {_eventPlannerUsername} has been deleted successfully.");
            }
        }
    }
}
