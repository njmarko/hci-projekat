using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class UpdatePartnerCommand : ICommand
    {

        private readonly UpdatePartnerViewModel _updatePartnerVm;
        private readonly IPartnersService _partnerService;


        public event EventHandler CanExecuteChanged;

        public UpdatePartnerCommand(UpdatePartnerViewModel updatePartnerVm, IPartnersService partnerService)
        {
            _updatePartnerVm = updatePartnerVm;
            _partnerService = partnerService;
            _updatePartnerVm.PropertyChanged += _updateProfileVm_PropertyChanged;

        }

        private void _updateProfileVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UpdateProfileViewModel.CanUpdate))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _updatePartnerVm.CanUpdate;
        }

        public void Execute(object parameter)
        {
            try
            {
                var user = _partnerService.Update(_updatePartnerVm.Partner.Id, _updatePartnerVm.Username, _updatePartnerVm.FirstName, _updatePartnerVm.LastName, _updatePartnerVm.DateOfBirth);

                _updatePartnerVm.Context.Store.CurrentUser = user;
                _updatePartnerVm.Partner = user;
                _updatePartnerVm.Username = user.Username;
                _updatePartnerVm.FirstName = user.FirstName;
                _updatePartnerVm.LastName = user.LastName;
                _updatePartnerVm.DateOfBirth = user.DateOfBirth;
                _updatePartnerVm.Context.Notifier.ShowSuccess("Profile info successfully updated.");
                //((Window)parameter).Close(); nesto ne moze, javlja null pointer exception


            }
            catch (UsernameAlreadyExistsException exception)
            {
                _updatePartnerVm.UsernameError.ErrorMessage = exception.Message;
            }
        }
    }
}
