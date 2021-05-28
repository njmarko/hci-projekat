using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.Validators
{
    public abstract class DirtyBitValidatorBase : ValidationRule
    {
        protected bool IsDirty { get; set; } = false;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!IsDirty)
            {
                IsDirty = true;
                return ValidationResult.ValidResult;
            }
            return ValidateInternal(value, cultureInfo);
        }

        protected abstract ValidationResult ValidateInternal(object value, CultureInfo cultureInfo);
    }
}
