using Models;
using System;

namespace BusinessLayer
{
	public interface IContactService
	{
		Guid CreateContact(ContactModel contactModel);
		ContactModel GetContact(Guid contactId);
	}
}
