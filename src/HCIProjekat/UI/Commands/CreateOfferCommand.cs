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
using UI.ViewModels;

namespace UI.Commands
{
    public class CreateOfferCommand : ICommand
    {
        private readonly CreateOfferViewModel _createOfferVm;
        private readonly IOfferService _offerService;

        private int _offerId;
        private int _partnerId;

        public event EventHandler CanExecuteChanged;

        public CreateOfferCommand(CreateOfferViewModel createOfferViewModel, IOfferService offerService, int partnerId, int offerId)
        {
            _createOfferVm = createOfferViewModel;
            _offerService = offerService;
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
                var offer = _offerService.Get(_offerId);
                offer.Description = _createOfferVm.Description;
                offer.Image = _createOfferVm.ImageInBytes;
                offer.Name = _createOfferVm.Name;
                offer.Price = int.Parse(_createOfferVm.Price);
                offer.OfferType = (ServiceType)_createOfferVm.OfferTypeValue.Type;

                _offerService.Update(offer);
            }
            else
            {
                _offerService.Create(new Offer
                {
                    Description = _createOfferVm.Description,
                    Image = _createOfferVm.ImageInBytes,
                    Name = _createOfferVm.Name,
                    Price = int.Parse(_createOfferVm.Price),
                    OfferType = (ServiceType)_createOfferVm.OfferTypeValue.Type
            }, _partnerId);
            }

            ((Window)parameter).DialogResult = true;
            ((Window)parameter).Close();
        }
    }
}
