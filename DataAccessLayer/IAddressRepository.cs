using Entities;
using System;

namespace DataAccessLayer
{
	public interface IAddressRepository
	{
		Guid CreateCompanyAddress(CompanyAddress address);
		Guid CreateContactAddress(ContactAddress address);
		Guid UpsertCompanyAddress(CompanyAddress address);
		Guid UpsertContactAddress(ContactAddress address);
		CompanyAddress GetCompanyAddress(Guid addressId);
		ContactAddress GetContactAddress(Guid addressId);
	}
}
