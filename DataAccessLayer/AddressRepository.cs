using System;
using System.Linq;
using Entities;

namespace DataAccessLayer
{
	public class AddressRepository : IAddressRepository
	{
		IFakeObjectGenerator _fakeObjectGenerator;

		public AddressRepository(IFakeObjectGenerator fakeObjectGenerator)
		{
			_fakeObjectGenerator = fakeObjectGenerator;
		}

		public Guid CreateCompanyAddress(CompanyAddress address)
		{
			address.Id = Guid.NewGuid();

			_fakeObjectGenerator.CompanyAddresses.Add(address);

			return address.Id;
		}

		public Guid CreateContactAddress(ContactAddress address)
		{
			address.Id = Guid.NewGuid();

			_fakeObjectGenerator.ContactAddresses.Add(address);

			return address.Id;
		}

		public CompanyAddress GetCompanyAddress(Guid addressId)
		{
			return _fakeObjectGenerator.CompanyAddresses.FirstOrDefault(address => address.Id == addressId);
		}

		public ContactAddress GetContactAddress(Guid addressId)
		{
			return _fakeObjectGenerator.ContactAddresses.FirstOrDefault(address => address.Id == addressId);
		}

		public Guid UpsertCompanyAddress(CompanyAddress address)
		{
			var original = _fakeObjectGenerator.CompanyAddresses.FirstOrDefault(add => add.Id == address.Id);

			if (original != null)
				original = address;

			address.Id = Guid.NewGuid();
			_fakeObjectGenerator.CompanyAddresses.Add(address);

			return address.Id;
		}

		public Guid UpsertContactAddress(ContactAddress address)
		{
			var original = _fakeObjectGenerator.ContactAddresses.FirstOrDefault(add => add.Id == address.Id);

			if (original != null)
				original = address;

			address.Id = Guid.NewGuid();
			_fakeObjectGenerator.ContactAddresses.Add(address);

			return address.Id;
		}
	}
}
