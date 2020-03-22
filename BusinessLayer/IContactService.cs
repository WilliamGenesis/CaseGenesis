using Models;
using System;

namespace BusinessLayer
{
	public interface IContactService
	{
		Guid CreateContact(ContactModel contactModel);
		Guid UpdateContact(ContactModel contactModel);
		bool DeleteContact(Guid contactId);
		ContactModel GetContact(Guid contactId);
		ContactModel[] GetAllContacts();
	}
}
