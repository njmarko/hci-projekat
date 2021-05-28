using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }

        private void _createOfferVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _offerService.Create(new Offer { Description = _createOfferVm.Description, Image = _createOfferVm.ImageInBytes, Name = _createOfferVm.Name, 
            Price = _createOfferVm.Price, OfferType = ServiceType.LOCATION});
        }
    }
}
