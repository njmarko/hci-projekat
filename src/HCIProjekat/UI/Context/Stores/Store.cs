using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Context.Stores
{
    public class Store : IStore, INotifyPropertyChanged
    {
        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set 
            { 
                _currentUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUser)));
                CurrentUserChanged?.Invoke(_currentUser);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<User> CurrentUserChanged;
    }
}
