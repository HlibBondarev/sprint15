using System.ComponentModel.DataAnnotations;

namespace ProductsValidation.Models.ModelValidationAttributes
{
	// TODO: + Create a validation attribute for the condition - Description should not be equal to Name but should start with the Name of the product
	public class DescriptionNameValidationAttribute : ValidationAttribute
	{
		public DescriptionNameValidationAttribute()
		{
			ErrorMessage = "Description should not be equal to Name but should start with the Name of the product!";
		}

		public override bool IsValid(object? value)
		{
			if (value is not Product product)
			{
				ErrorMessage = "The parameter must be a product type!";
				return false;
			}
			if (string.IsNullOrEmpty(product.Description)) return true;
			if (product.Description.StartsWith(product.Name))
			{
				return product.Description != product.Name.Trim();
			}
			return false;
		}
	}
}
