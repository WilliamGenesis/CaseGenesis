using BusinessLayer;
using CaseGenesis.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;

namespace CaseGenesisUnitTests
{
	[TestClass]
	public class ContactControllerUnitTests
	{
		[TestMethod]
		public void CreateContact_GivenAValidContactModel_ShouldReturnAOkActionResult()
		{
			//Arrange
			var expectedGuid = Guid.NewGuid();
			var contact = new ContactModel
			{
				Address = GetDummyAddress(),
				Companies = new[] { GetValidCompany() },
				IsFreelance = true,
				TvaNumber = "TvaNumber"
			};

			var contactServiceMock = new Mock<IContactService>();
			contactServiceMock.Setup(service => service.CreateContact(It.IsAny<ContactModel>()))
				.Returns(expectedGuid);

			var contactController = new ContactController(contactServiceMock.Object);

			//Act
			var result = contactController.CreateContact(contact);
			var okResult = result as OkObjectResult;

			//Assert
			contactServiceMock.Verify(service => service.CreateContact(It.Is<ContactModel>(
				model => model.Id == contact.Id)), Times.Once);
			Assert.IsNotNull(okResult);
			Assert.AreEqual(expectedGuid, okResult.Value);
		}

		[TestMethod]
		public void CreateContact_GivenAInValidContactModelWithEmptyAddress_ShouldReturnABadRequestActionResult()
		{
			//Arrange
			var contact = new ContactModel
			{
				Companies = new[] { GetValidCompany() },
				IsFreelance = true,
				TvaNumber = "TvaNumber"
			};

			var contactController = new ContactController(null);
			contactController.ModelState.AddModelError("Error", "Address is required");

			//Act
			var result = contactController.CreateContact(contact);
			var badRequestObjectResult = result as BadRequestObjectResult;

			//Assert
			Assert.IsNotNull(badRequestObjectResult);
		}

		private CompanyModel GetValidCompany()
		{
			return new CompanyModel
			{
				MainAddress = GetDummyAddress(),
				OtherAddresses = null,
				TvaNumber = "TvaNumber"
			};
		}

		private AddressModel GetDummyAddress()
		{
			return new AddressModel { Id = Guid.NewGuid(), Address = "address", PostalCode = "1234", Country = "Belgium" }; 
		}
	}
}
