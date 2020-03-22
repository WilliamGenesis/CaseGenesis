using System;
using System.Linq;
using Entities;

namespace DataAccessLayer
{
	public class ContactRepository : IContactRepository
	{
		IFakeObjectGenerator _fakeObjectGenerator;

		public ContactRepository(IFakeObjectGenerator fakeObjectGenerator)
		{
			_fakeObjectGenerator = fakeObjectGenerator;
		}

		public Guid CreateContact(Contact contact)
		{
			contact.Id = Guid.NewGuid();
			_fakeObjectGenerator.Contacts.Add(contact);

			return contact.Id;
		}

		public bool DeleteContact(Guid contactId)
		{
			var original = _fakeObjectGenerator.Contacts.FirstOrDefault(cont => cont.Id == contactId);

			if (original == null)
				return false;

			_fakeObjectGenerator.Contacts.Remove(original);
			return true;
		}

		public Contact GetContact(Guid contactId)
		{
			return _fakeObjectGenerator.Contacts.FirstOrDefault(contact => contact.Id == contactId);
		}

		public Guid UpdateContact(Contact contact)
		{
			var original = _fakeObjectGenerator.Contacts.FirstOrDefault(cont => cont.Id == contact.Id);

			if (original == null)
				throw new Exception("The contact you are trying to update does not exist");

			original = contact;

			return contact.Id;
		}
	}
}
