using Entities;
using System;

namespace DataAccessLayer
{
	public interface IContactRepository
	{
		Guid CreateContact(Contact contact);
		Contact GetContact(Guid contactId);
	}
}
