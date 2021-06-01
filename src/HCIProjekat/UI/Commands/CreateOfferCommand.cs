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
        private readonly IModalService _modalService;

        private int _offerId;
        private int _partnerId;

        public event EventHandler CanExecuteChanged;

        public CreateOfferCommand(PartnerOffersViewModel partnerVm, CreateOfferViewModel createOfferViewModel, IOfferService offerService, IModalService modalService, int partnerId, int offerId)
        {
            _partnerVm = partnerVm;
            _createOfferVm = createOfferViewModel;
            _offerService = offerService;
            _modalService = modalService;
            _offerId = offerId;
            _partnerId = partnerId;
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
                DoAction(parameter, "Are you sure you want to update this offer?", UpdateOffer);
            }
            else
            {
                DoAction(parameter, "Are you sure you want to create this offer?", CreateOffer);
            }
        }

        private void DoAction(object parameter, string confirmMessage, Action action)
        {
            var ok = _modalService.ShowConfirmationDialog(confirmMessage);
            if (ok)
            {
                action();
                ((Window)parameter).DialogResult = true;
                ((Window)parameter).Close();
            }
        }

        private void UpdateOffer()
        {
            var offer = _offerService.Get(_offerId);
            _partnerVm.AddItem(offer);

            offer.Description = _createOfferVm.DescriptionField;
            offer.Image = _createOfferVm.ImageInBytesField;
            offer.Name = _createOfferVm.NameField;
            offer.Price = int.Parse(_createOfferVm.PriceField);
            offer.OfferType = (ServiceType)_createOfferVm.OfferTypeValue.Type;

            _offerService.Update(offer);
        }

        private void CreateOffer()
        {
            var offer = _offerService.Create(new Offer
            {
                Description = _createOfferVm.DescriptionField,
                Image = _createOfferVm.ImageInBytesField,
                Name = _createOfferVm.NameField,
                Price = int.Parse(_createOfferVm.PriceField),
                OfferType = (ServiceType)_createOfferVm.OfferTypeValue.Type
            }, _partnerId);

            offer.Active = false;
            _partnerVm.AddItem(offer);
        }
    }
}
