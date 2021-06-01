using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public abstract class ValidationModel<T> : ViewModelBase
    {
        private IDictionary<string, bool> _dirtyValues;
        private int _numOfFields = 0;

        public ValidationModel(IApplicationContext context) : base(context)
        {
            InitValues();
        }

        private void InitValues()
        {
            _dirtyValues = new Dictionary<string, bool>();

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name.ToLower().Contains("field"))
                {
                    _dirtyValues.Add(prop.Name, false);
                    _numOfFields++;
                }
            }
        }

        protected new void OnPropertyChanged(string propertyName)
        {
            if (_dirtyValues.ContainsKey(propertyName))
            {
                _dirtyValues[propertyName] = true;
            }

            base.OnPropertyChanged(propertyName);
        }

        protected bool IsDirty(string propertyName)
        {
            return _dirtyValues[propertyName];
        }

        protected bool AllDirty()
        {
            System.Console.WriteLine(_dirtyValues.Values.Where(v => v).Count());
            return _dirtyValues.Values.Where(v => v).Count() == _numOfFields;
        }
    }
}
