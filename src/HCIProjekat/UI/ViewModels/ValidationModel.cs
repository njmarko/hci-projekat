using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UI.Context;
using UI.CustomAttributes;

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

            var properties = typeof(T).GetProperties()
                                      .Where(prop => Attribute.IsDefined(prop, typeof(ValidationField)));
            foreach (var prop in properties)
            {
                _dirtyValues.Add(prop.Name, false);
                _numOfFields++;
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

        protected void ResetDirtyValues()
        {
            foreach(var key in _dirtyValues.Keys)
            {
                _dirtyValues[key] = false;
            }
        }

        protected bool IsDirty(string propertyName)
        {
            return _dirtyValues[propertyName];
        }

        protected bool AllDirty()
        {
            return _dirtyValues.Values.Where(v => v).Count() == _numOfFields;
        }
    }
}
