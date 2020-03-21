using Entities;
using System;

namespace DataAccessLayer
{
	public interface IContactRepository
	{
		Guid CreateContact(Contact contact);
		Guid UpdateContact(Contact contact);
		bool DeleteContact(Guid contactId);
		Contact GetContact(Guid contactId);
	}
}
