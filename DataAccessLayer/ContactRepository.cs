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
			_fakeObjectGenerator.Contacts.Add(ResolveInsertContact(contact));

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

		public Contact[] GetAllContacts()
		{
			var contacts = _fakeObjectGenerator.Contacts.ToArray();

			return contacts.Select(contact => ResolveContact(contact)).ToArray();
		}

		public Contact GetContact(Guid contactId)
		{
			return ResolveContact(_fakeObjectGenerator.Contacts.FirstOrDefault(contact => contact.Id == contactId));
		}

		public Guid UpdateContact(Contact contact)
		{
			var original = _fakeObjectGenerator.Contacts.FirstOrDefault(cont => cont.Id == contact.Id);

			if (original == null)
				throw new Exception("The contact you are trying to update does not exist");

			_fakeObjectGenerator.Contacts.Remove(original);
			_fakeObjectGenerator.Contacts.Add(ResolveInsertContact(contact, false));

			return contact.Id;
		}

		#region HelpersToSimulateEF

		public Contact ResolveInsertContact(Contact contact, bool overrideId = true)
		{
			contact.Address = null;
			contact.Companies = null;

			contact.Id = overrideId ? _fakeObjectGenerator.GetNewGuid() : contact.Id ;

			return contact;
		}

		public Contact ResolveContact(Contact contact)
		{
			if (contact is null)
				return null;

			contact.Address = _fakeObjectGenerator.ContactAddresses.FirstOrDefault(address => address.ContactId == contact.Id);
			contact.Companies = _fakeObjectGenerator.Companies.Where(company => company.ContactId == contact.Id)
				.Select(company => ResolveCompany(company)).ToArray();

			return contact;
		}

		public Company ResolveCompany(Company company)
		{
			company.MainAddress = _fakeObjectGenerator.CompanyAddresses.FirstOrDefault(address => address.Id == company.MainAddressId);
			company.OtherAddresses = _fakeObjectGenerator.CompanyAddresses.Where(address => address.CompanyId == company.Id && !address.IsMainAddress).ToArray();

			return company;
		}

		#endregion
	}
}
