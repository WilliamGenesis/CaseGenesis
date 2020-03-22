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
	public class CompanyServiceUnitTests
	{
		[TestMethod]
		public void CreateCompany_GivenAValidCompany_ShouldCallAddressRepositoryUpsertCompanyAddressOncePlusOtherAddressesCount()
		{
			//Arrange
			var company = GetCorrectCompanyModel();

			var companyRepositoryMock = new Mock<ICompanyRepository>();
			companyRepositoryMock.Setup(repository => repository.CreateCompany(It.IsAny<Company>()))
				.Returns(Guid.NewGuid());

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertCompanyAddress(It.IsAny<CompanyAddress>()))
				.Returns(Guid.NewGuid());

			var companyService = new CompanyService(companyRepositoryMock.Object, addressRepositoryMock.Object);

			//Act
			var result = companyService.CreateCompany(company);

			//Assert
			addressRepositoryMock.Verify(repository => repository.UpsertCompanyAddress(It.IsAny<CompanyAddress>()), Times.Exactly(1 + company.OtherAddresses.Length));
		}

		[TestMethod]
		public void CreateCompany_GivenAValidCompany_ShouldCallCompanyRepositoryCreateCompanyOnce()
		{
			//Arrange
			var company = GetCorrectCompanyModel();

			var companyRepositoryMock = new Mock<ICompanyRepository>();
			companyRepositoryMock.Setup(repository => repository.CreateCompany(It.IsAny<Company>()))
				.Returns(company.Id);

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertCompanyAddress(It.IsAny<CompanyAddress>()))
				.Returns(Guid.NewGuid());

			var companyService = new CompanyService(companyRepositoryMock.Object, addressRepositoryMock.Object);

			//Act
			var result = companyService.CreateCompany(company);

			//Assert
			companyRepositoryMock.Verify(repository => repository.CreateCompany(It.Is<Company>(
				companyEntity => companyEntity.Id == company.Id)), Times.Once);
		}

		[TestMethod]
		public void UpsertCompany_GivenAValidCompany_ShouldCallAddressRepositoryUpsertCompanyAddressOncePlusOtherAddressesCount()
		{
			//Arrange
			var company = GetCorrectCompanyModel();

			var companyRepositoryMock = new Mock<ICompanyRepository>();
			companyRepositoryMock.Setup(repository => repository.CreateCompany(It.IsAny<Company>()))
				.Returns(Guid.NewGuid());
			companyRepositoryMock.Setup(repository => repository.GetCompany(It.IsAny<Guid>()))
				.Returns((Company)null);

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertCompanyAddress(It.IsAny<CompanyAddress>()))
				.Returns(Guid.NewGuid());

			var companyService = new CompanyService(companyRepositoryMock.Object, addressRepositoryMock.Object);

			//Act
			var result = companyService.UpsertCompany(company);

			//Assert
			addressRepositoryMock.Verify(repository => repository.UpsertCompanyAddress(It.IsAny<CompanyAddress>()), Times.Exactly(1 + company.OtherAddresses.Length));
		}

		[TestMethod]
		public void UpsertCompany_GivenAValidCompanyThatDoNotExistYet_ShouldCallCompanyRepositoryCreateCompanyOnce()
		{
			//Arrange
			var company = GetCorrectCompanyModel();

			var companyRepositoryMock = new Mock<ICompanyRepository>();
			companyRepositoryMock.Setup(repository => repository.CreateCompany(It.IsAny<Company>()))
				.Returns(Guid.NewGuid());
			companyRepositoryMock.Setup(repository => repository.GetCompany(It.IsAny<Guid>()))
				.Returns((Company)null);

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertCompanyAddress(It.IsAny<CompanyAddress>()))
				.Returns(Guid.NewGuid());

			var companyService = new CompanyService(companyRepositoryMock.Object, addressRepositoryMock.Object);

			//Act
			var result = companyService.UpsertCompany(company);

			//Assert
			companyRepositoryMock.Verify(repository => repository.CreateCompany(It.IsAny<Company>()), Times.Once);
		}

		[TestMethod]
		public void UpsertCompany_GivenAValidCompanyThatAlreadyExist_ShouldCallCompanyRepositoryUpdateCompanyOnce()
		{
			//Arrange
			var company = GetCorrectCompanyModel();

			var companyRepositoryMock = new Mock<ICompanyRepository>();
			companyRepositoryMock.Setup(repository => repository.UpdateCompany(It.IsAny<Company>()))
				.Returns(Guid.NewGuid());
			companyRepositoryMock.Setup(repository => repository.GetCompany(It.IsAny<Guid>()))
				.Returns(new Company());

			var addressRepositoryMock = new Mock<IAddressRepository>();
			addressRepositoryMock.Setup(repository => repository.UpsertCompanyAddress(It.IsAny<CompanyAddress>()))
				.Returns(Guid.NewGuid());

			var companyService = new CompanyService(companyRepositoryMock.Object, addressRepositoryMock.Object);

			//Act
			var result = companyService.UpsertCompany(company);

			//Assert
			companyRepositoryMock.Verify(repository => repository.UpdateCompany(It.IsAny<Company>()), Times.Once);
		}

		private CompanyModel GetCorrectCompanyModel()
		{
			return new CompanyModel
			{
				Id = Guid.NewGuid(),
				MainAddress = new AddressModel { Id = Guid.NewGuid(), Address = "address", PostalCode = "1234", Country = "Belgium" },
				OtherAddresses = new[]
				{
					new AddressModel { Id = Guid.NewGuid(), Address = "address", PostalCode = "1234", Country = "Belgium" },
					new AddressModel { Id = Guid.NewGuid(), Address = "address2", PostalCode = "1235", Country = "Belgium" },
					new AddressModel { Id = Guid.NewGuid(), Address = "address3", PostalCode = "1236", Country = "Belgium" },
				},
				TvaNumber = "TvaNumber"
			};
		}
	}
}
