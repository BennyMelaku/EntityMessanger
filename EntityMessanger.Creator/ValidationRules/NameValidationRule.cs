using System.Globalization;
using System.Windows.Controls;

namespace EntityMessanger.Creator.ValidationRules
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) =>
            string.IsNullOrEmpty(value as string) 
            ? new ValidationResult(false, "Name is required.") 
            : ValidationResult.ValidResult;
    }

}
