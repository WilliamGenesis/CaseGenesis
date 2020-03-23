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
			contactModel.Id = _contactRepository.CreateContact(contact);

			_addressRepository.UpsertContactAddress(contactModel.Address.ToContactAddress(contactModel));

			foreach (var company in contactModel.Companies)
			{
				company.ContactId = contactModel.Id;
				_companyService.UpsertCompany(company);
			}

			return contactModel.Id;
		}

		public bool DeleteContact(Guid contactId)
		{
			return _contactRepository.DeleteContact(contactId);
		}

		public ContactModel[] GetAllContacts()
		{
			return _contactRepository.GetAllContacts()?.ToContactModels();
		}

		public ContactModel GetContact(Guid contactId)
		{
			return _contactRepository.GetContact(contactId)?.ToContactModel();
		}

		public Guid UpdateContact(ContactModel contactModel)
		{
			var contact = contactModel.ToContact();
			contactModel.Id = _contactRepository.UpdateContact(contact);

			_addressRepository.UpdateContactAddress(contactModel.Address.ToContactAddress(contactModel));

			foreach (var company in contactModel.Companies)
			{
				company.ContactId = contactModel.Id;
				_companyService.UpsertCompany(company);
			}

			return contactModel.Id;
		}
	}
}
