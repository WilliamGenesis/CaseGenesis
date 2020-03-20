using System.ComponentModel.DataAnnotations;

namespace Models.Validation
{
	public static class ContactValidation
	{
		public static ValidationResult[] ValidateContactModel(ContactModel contactModel)
		{
			return new[]
			{
				ValidateTvaNumber(contactModel)
			};
		}

		private static ValidationResult ValidateTvaNumber(ContactModel contactModel)
		{
			if (contactModel.IsFreelance && string.IsNullOrEmpty(contactModel.TvaNumber))
				return new ValidationResult($"{nameof(ContactModel.TvaNumber)} is mandatory when {nameof(ContactModel.IsFreelance)} is true");

			return ValidationResult.Success;
		}
	}
}
