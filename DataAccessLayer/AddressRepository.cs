using System;
using Entities;

namespace DataAccessLayer
{
	public class AddressRepository : IAddressRepository
	{
		public Guid CreateCompanyAddress(CompanyAddress address)
		{
			throw new NotImplementedException();
		}

		public Guid CreateContactAddress(ContactAddress address)
		{
			throw new NotImplementedException();
		}

		public CompanyAddress GetCompanyAddress(Guid address)
		{
			throw new NotImplementedException();
		}

		public CompanyAddress GetContactAddress(Guid address)
		{
			throw new NotImplementedException();
		}

		public Guid UpsertCompanyAddress(CompanyAddress address)
		{
			throw new NotImplementedException();
		}

		public Guid UpsertContactAddress(ContactAddress address)
		{
			throw new NotImplementedException();
		}
	}
}
