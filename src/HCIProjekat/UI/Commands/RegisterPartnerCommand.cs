using Domain.Entities;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Context;
using UI.Context.Routers;
using UI.ViewModels;

namespace UI.Commands
{
    public class RegisterPartnerCommand : ICommand
    {
        private readonly RegisterPartnerViewModel _registerVm;
        private readonly IPartnersService _partnerService;
        private readonly IRouter _router;
        private readonly IApplicationContext _context;
        public event EventHandler CanExecuteChanged;
        private readonly int _partnerId;

        public RegisterPartnerCommand(RegisterPartnerViewModel registerVm, IPartnersService partnerService, IRouter router, IApplicationContext context, int partnerId)
        {
            _registerVm = registerVm;
            _partnerService = partnerService;
            _router = router;
            _registerVm.PropertyChanged += _registerVm_PropertyChanged;
            _context = context;
            _partnerId = partnerId;
        }

        private void _registerVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegisterPartnerViewModel.CanRegister))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _registerVm.CanRegister;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (_partnerId == -1)
                {
                    Create();
                }
                else
                {
                    Update();
                    (parameter as Window).DialogResult = true;
                    (parameter as Window).Close();
                }
            }
            catch (PartnerAlreadyExistsException exception)
            {
                _registerVm.NameError.ErrorMessage = exception.Message;
            }
        }

        private void Create()
        {
            var registerPartner = _partnerService.Create(new Partner
            {
                Name = _registerVm.Name,
                Type = (Domain.Enums.PartnerType)_registerVm.PartnerTypeValue.Type,
                Location = new Location { City = _registerVm.City, Country = _registerVm.Country, Street = _registerVm.Street, StreetNumber = _registerVm.StreetNumber }
            });
            _context.Notifier.ShowInformation($"Partner {registerPartner.Name} sucessfuly added.");
            _router.Push("AdminPartners");
        }

        private void Update()
        {
            var updatedPartner = _partnerService.Update(_partnerId, _registerVm.Name, _registerVm.PartnerTypeValue.Type.Value, _registerVm.Country, _registerVm.City, _registerVm.Street, _registerVm.StreetNumber);
            _context.Notifier.ShowInformation($"Partner {updatedPartner.Name} information sucessfuly updated.");
            _router.Push("AdminPartners");
        }
    }
}
