using System;
using Models;
using BusinessLayer.Mapping;
using DataAccessLayer;

namespace BusinessLayer
{
	public class ContactService : IContactService
	{
		private readonly IContactRepository _contactRepository;
		private readonly ICompanyService _companyService;
		private readonly IAddressRepository _addressRepository;

		public ContactService(IContactRepository contactRepository, ICompanyService companyService, IAddressRepository addressRepository)
		{
			_contactRepository = contactRepository;
			_companyService = companyService;
			_addressRepository = addressRepository;
		}

		public Guid CreateContact(ContactModel contactModel)
		{
			var contact = contactModel.ToContact();

			_addressRepository.UpsertContactAddress(contact.Address);

			foreach (var company in contactModel.Companies)
			{
				_companyService.UpsertCompany(company);
			}

			return _contactRepository.CreateContact(contact);
		}

		public bool DeleteContact(Guid contactId)
		{
			return DeleteContact(contactId);
		}

		public ContactModel GetContact(Guid contactId)
		{
			return _contactRepository.GetContact(contactId).ToContactModel();
		}

		public Guid UpdateContact(ContactModel contactModel)
		{
			var contact = contactModel.ToContact();

			return _contactRepository.UpdateContact(contact);
		}
	}
}
