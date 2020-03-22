using Bogus;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
	public class FakeDataGenerator
	{
		private const int CONTACT_COUNT = 20;
		private readonly Faker _faker = new Faker("en");

		public FakeDataGenerator()
		{
			Randomizer.Seed = new Random(444719);
		}

		public List<Contact> Contacts { get; set; }
		public List<Company> Companies { get; set; }
		public List<ContactAddress> ContactAddresses { get; set; }
		public List<CompanyAddress> CompanyAddresses { get; set; }

		private void CreateDataset()
		{
			Contacts = new List<Contact>();
			Companies = new List<Company>();
			CompanyAddresses = new List<CompanyAddress>();
			ContactAddresses = new List<ContactAddress>();

			for (var i = 0; i < CONTACT_COUNT; i++)
			{
				var guid = _faker.Random.Guid();
				var isFreelance = _faker.Random.Bool();

				new Contact
				{
					Id = guid,
					IsFreelance = isFreelance,
					TvaNumber = isFreelance ? $"BE{_faker.Random.Long(1000000000, 9999999999).ToString()}" : string.Empty
				};

				CreateContactAddress(guid);
				CreateCompanies(_faker.Random.Int(1, 3), guid);
			}
		}

		private void CreateCompanies(int count, Guid contactId)
		{
			for(var i = 0; i < count; i++)
			{
				var guid = _faker.Random.Guid();
				var mainAddressId = CreateMainCompanyAddress(guid);

				Companies.Add(
					new Company
					{
						Id = guid,
						ContactId = contactId,
						TvaNumber = $"BE{_faker.Random.Long(1000000000, 9999999999).ToString()}",
						MainAddressId = mainAddressId
					});

				CreateCompanyAddresses(_faker.Random.Int(1, 3), guid);
			}
		}

		private void CreateContactAddress(Guid contactId)
		{
			ContactAddresses.Add(
				new ContactAddress
				{
					Id = _faker.Random.Guid(),
					Address = _faker.Address.FullAddress(),
					PostalCode = _faker.Address.ZipCode(),
					Country = "Belgium",
					ContactId = contactId
				});
		}

		private Guid CreateMainCompanyAddress(Guid companyId)
		{
			var guid = _faker.Random.Guid();
			CompanyAddresses.Add(
				new CompanyAddress
				{
					Id = guid,
					Address = _faker.Address.FullAddress(),
					PostalCode = _faker.Address.ZipCode(),
					Country = "Belgium",
					CompanyId = companyId,
					IsMainAddress = true
				});

			return guid;
		}

		private void CreateCompanyAddresses(int count, Guid companyId)
		{
			for (var i = 0; i < count; i++)
			{
				CompanyAddresses.Add(
					new CompanyAddress
					{
						Id = _faker.Random.Guid(),
						Address = _faker.Address.FullAddress(),
						PostalCode = _faker.Address.ZipCode(),
						Country = "Belgium",
						CompanyId = companyId,
						IsMainAddress = false
					});
			}
		}
	}
}