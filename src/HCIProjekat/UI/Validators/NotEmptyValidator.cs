using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.Validators
{
    public class NotEmptyValidator : DirtyBitValidatorBase
    {
        public string FieldName { get; set; }

        public NotEmptyValidator()
        {

        }

        protected override ValidationResult ValidateInternal(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value?.ToString() ?? ""))
            {
                return new ValidationResult(false, $"{FieldName} cannot be empty.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
