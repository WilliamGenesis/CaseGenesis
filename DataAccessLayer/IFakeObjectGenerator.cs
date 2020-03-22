using Entities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
	public interface IFakeObjectGenerator
	{
		List<Contact> Contacts { get; set; }
		List<Company> Companies { get; set; }
		List<ContactAddress> ContactAddresses { get; set; }
		List<CompanyAddress> CompanyAddresses { get; set; }
		Guid GetNewGuid();
	}
}
