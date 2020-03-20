using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayerUnitTests
{
	[TestClass]
	public class ContactValidationUnitTests
	{
		private const int FREELANCE_TVA_VALIDATION_INDEX = 0;


		[TestMethod]
		public void ValidateContactModel_GivenAFreelanceContactModelWithoutTvaNumber_ShouldReturnAFailedValidationResult()
		{
			//Arrange
			var contact = new ContactModel
			{
				IsFreelance = true
			};

			//Act
			var result = ContactValidation.ValidateContactModel(contact);

			//Assert
			Assert.IsFalse(result[FREELANCE_TVA_VALIDATION_INDEX] == ValidationResult.Success);
		}

		[TestMethod]
		public void ValidateContactModel_GivenAFreelanceContactModelWithTvaNumber_ShouldReturnASuccessValidationResult()
		{
			//Arrange

			var contact = new ContactModel
			{
				IsFreelance = true,
				TvaNumber = "TvaNumber"
			};

			//Act
			var result = ContactValidation.ValidateContactModel(contact);

			//Assert
			Assert.IsTrue(result[FREELANCE_TVA_VALIDATION_INDEX] == ValidationResult.Success);
		}

		[TestMethod]
		public void ValidateContactModel_GivenANonFreelanceContactModelWithoutTvaNumber_ShouldReturnASuccessValidationResult()
		{
			//Arrange

			var contact = new ContactModel
			{
				IsFreelance = false,
				TvaNumber = "TvaNumber"
			};

			//Act
			var result = ContactValidation.ValidateContactModel(contact);

			//Assert
			Assert.IsTrue(result[FREELANCE_TVA_VALIDATION_INDEX] == ValidationResult.Success);
		}
	}
}
