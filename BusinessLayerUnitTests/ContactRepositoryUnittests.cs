using BusinessLayer;
using DataAccessLayer;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;

namespace BusinessLayerUnitTests
{
	[TestClass]
	public class ContactRepositoryUnittests
	{
		[TestMethod]
		public void CreateContact_GivenACorrectContactModel_ShouldCallUpsertCompanyForEveryCompany()
		{
			//Arrange
			var contact = GetCorrectContactModel();

			var companyServiceMock = new Mock<ICompanyService>();
			companyServiceMock.Setup(service => service.UpsertCompany(It.IsAny<CompanyModel>()))
				.Returns(Guid.NewGuid());

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertContactAddress(It.IsAny<ContactAddress>()))
				.Returns(Guid.NewGuid());

			var contactRepositoryMock = new Mock<IContactRepository>();
			contactRepositoryMock.Setup(repository => repository.CreateContact(It.IsAny<Contact>()))
				.Returns(Guid.NewGuid());

			var contactService = new ContactService(contactRepositoryMock.Object, companyServiceMock.Object, addressRepositoryMock.Object);

			//Act
			var result = contactService.CreateContact(contact);

			//Assert
			companyServiceMock.Verify(service => service.UpsertCompany(It.IsAny<CompanyModel>()), Times.Exactly(contact.Companies.Length));
		}

		[TestMethod]
		public void CreateContact_GivenACorrectContactModel_ShouldCallUpsertContyactAddressOnce()
		{
			//Arrange
			var contact = GetCorrectContactModel();

			var companyServiceMock = new Mock<ICompanyService>();
			companyServiceMock.Setup(service => service.UpsertCompany(It.IsAny<CompanyModel>()))
				.Returns(Guid.NewGuid());

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertContactAddress(It.IsAny<ContactAddress>()))
				.Returns(Guid.NewGuid());

			var contactRepositoryMock = new Mock<IContactRepository>();
			contactRepositoryMock.Setup(repository => repository.CreateContact(It.IsAny<Contact>()))
				.Returns(Guid.NewGuid());

			var contactService = new ContactService(contactRepositoryMock.Object, companyServiceMock.Object, addressRepositoryMock.Object);

			//Act
			var result = contactService.CreateContact(contact);

			//Assert
			addressRepositoryMock.Verify(repository => repository.UpsertContactAddress(It.IsAny<ContactAddress>()), Times.Once);
		}

		[TestMethod]
		public void CreateContact_GivenACorrectContactModel_ShouldCallCreateContactOnce()
		{
			//Arrange
			var contact = GetCorrectContactModel();

			var companyServiceMock = new Mock<ICompanyService>();
			companyServiceMock.Setup(service => service.UpsertCompany(It.IsAny<CompanyModel>()))
				.Returns(Guid.NewGuid());

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertContactAddress(It.IsAny<ContactAddress>()))
				.Returns(Guid.NewGuid());

			var contactRepositoryMock = new Mock<IContactRepository>();
			contactRepositoryMock.Setup(repository => repository.CreateContact(It.IsAny<Contact>()))
				.Returns(Guid.NewGuid());

			var contactService = new ContactService(contactRepositoryMock.Object, companyServiceMock.Object, addressRepositoryMock.Object);

			//Act
			var result = contactService.CreateContact(contact);

			//Assert
			contactRepositoryMock.Verify(repository => repository.CreateContact(
				It.Is<Contact>(contactEntity => contactEntity.Id == contact.Id)), Times.Once);
		}

		private ContactModel GetCorrectContactModel()
		{
			return new ContactModel
			{
				Id = Guid.NewGuid(),
				Address = new AddressModel { Id = Guid.NewGuid(), Address = "address", PostalCode = "1234", Country = "Belgium" },
				Companies = new CompanyModel[]
				{
					new CompanyModel
					{
						Id = Guid.NewGuid(),
						MainAddress = new AddressModel { Id = Guid.NewGuid(), Address = "address", PostalCode = "1234", Country = "Belgium" },
						TvaNumber = "TvaNumber"
					},
					new CompanyModel
					{
						Id = Guid.NewGuid(),
						MainAddress = new AddressModel { Id = Guid.NewGuid(), Address = "address", PostalCode = "1234", Country = "Belgium" },
						TvaNumber = "TvaNumber2"
					}
				},
				IsFreelance = false
			};
		}
	}
}
