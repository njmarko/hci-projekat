using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Commands
{
    public class CreateOfferCommand : ICommand
    {
        private readonly PartnerOffersViewModel _partnerVm;
        private readonly CreateOfferViewModel _createOfferVm;
        private readonly IOfferService _offerService;
        private readonly ISeatingLayoutService _seatingLayoutService;

        private int _offerId;
        private int _partnerId;
        private SeatingLayout _seatingLayout;

        public event EventHandler CanExecuteChanged;

        public CreateOfferCommand(PartnerOffersViewModel partnerVm, CreateOfferViewModel createOfferViewModel, IOfferService offerService, ISeatingLayoutService seatingLayoutService, int partnerId, int offerId, SeatingLayout seatingLayout)
        {
            _partnerVm = partnerVm;
            _createOfferVm = createOfferViewModel;
            _offerService = offerService;
            _seatingLayoutService = seatingLayoutService;
            _offerId = offerId;
            _partnerId = partnerId;
            _seatingLayout = seatingLayout;
            _createOfferVm.PropertyChanged += _createOfferVm_PropertyChanged;
        }

        private void _createOfferVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateOfferViewModel.CanCreateOffer))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _createOfferVm.CanCreateOffer;
        }

        public void Execute(object parameter)
        {                   
            if (_offerId != -1)
            {
                DoAction(parameter, UpdateOffer);
            }
            else
            {
                DoAction(parameter, CreateOffer);
            }
        }

        private void DoAction(object parameter, Action action)
        {
            action();
            ((Window)parameter).DialogResult = true;
            ((Window)parameter).Close();
        }

        private void UpdateOffer()
        {
            var offer = _offerService.Get(_offerId);
            _partnerVm.AddItem(offer);

            offer.Description = _createOfferVm.Description;
            offer.Image = _createOfferVm.ImageInBytes;
            offer.Name = _createOfferVm.Name;
            offer.Price = int.Parse(_createOfferVm.Price);
            offer.OfferType = (ServiceType)_createOfferVm.OfferTypeValue.Type;

            _offerService.Update(offer);
        }

        private void CreateOffer()
        {
            var offer = _offerService.Create(new Offer
            {
                Description = _createOfferVm.Description,
                Image = _createOfferVm.ImageInBytes,
                Name = _createOfferVm.Name,
                Price = int.Parse(_createOfferVm.Price),
                OfferType = (ServiceType)_createOfferVm.OfferTypeValue.Type
            }, _partnerId);
            if (offer.OfferType == ServiceType.LOCATION)
            {
                _seatingLayout.OfferId = offer.Id;
                _seatingLayoutService.Create(_seatingLayout);
            }

            offer.Active = false;
            _partnerVm.AddItem(offer);
        }
    }
}
