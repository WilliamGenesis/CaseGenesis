﻿using System;
using Models;
using BusinessLayer.Mapping;
using DataAccessLayer;

namespace BusinessLayer
{
	public class ContactService : IContactService
	{
		private readonly IContactRepository _contactRepository;

		public ContactService(IContactRepository contactRepository)
		{
			_contactRepository = contactRepository;
		}

		public Guid CreateContact(ContactModel contactModel)
		{
			var contact = contactModel.ToContact();

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
