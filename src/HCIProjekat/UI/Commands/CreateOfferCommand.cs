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

        public event EventHandler CanExecuteChanged;

        public CreateOfferCommand(CreateOfferViewModel createOfferViewModel, IOfferService offerService)
        {
            _createOfferVm = createOfferViewModel;
            _offerService = offerService;
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
            ((Window)parameter).Close();
            _offerService.Create(new Offer { Description = _createOfferVm.Description, Image = _createOfferVm.ImageInBytes, Name = _createOfferVm.Name, 
            Price = int.Parse(_createOfferVm.Price), OfferType = ServiceType.LOCATION}, 1);
        }
    }
}
