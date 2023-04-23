using System.Globalization;
using System.Windows.Controls;

namespace EntityMessanger.Creator.ValidationRules
{
    public class DoubleRangeValidationRule : ValidationRule
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double inputValue;
            if (value == null || !double.TryParse(value.ToString(), out inputValue))
                return new ValidationResult(false, "Value must be a valid double.");
            
            if (inputValue < MinValue || inputValue > MaxValue)
                return new ValidationResult(false, $"Value must be between {MinValue} and {MaxValue}.");
            
            return ValidationResult.ValidResult;
        }
    }

}
