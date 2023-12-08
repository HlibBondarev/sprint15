using System.ComponentModel.DataAnnotations;

namespace ProductsValidation.Models.ModelValidationAttributes
{
    // TODO: + Create a validation attribute for the condition - if set to Description, the length must be longer than 2 characters.
    public class DescriptionValidationAttribute : ValidationAttribute
	{
		readonly int _minimumLength = 0;
		public DescriptionValidationAttribute(int minimumLength)
		{
			_minimumLength = minimumLength;
			ErrorMessage = string.Format("Description length sould be longer than {0} symbols", minimumLength);
		}
		public override bool IsValid(object? value)
		{
			if (value is null) return true;
			return value.ToString().Length > _minimumLength;
		}
	}
}
