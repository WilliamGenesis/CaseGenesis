﻿using System;
using BusinessLayer;
using CaseGenesis.Utils;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Validation;

namespace CaseGenesis.Controllers
{
	[Route("api/contact")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private readonly IContactService _contactService;

		public ContactController(IContactService contactService)
		{
			_contactService = contactService;
		}

		[HttpGet]
		[Route("{contactId}")]
		public ActionResult Get(Guid contactId)
		{
			var contact = _contactService.GetContact(contactId);

			if (contact is null)
				return NotFound();

			return Ok(_contactService.GetContact(contactId));
		}

		[HttpGet]
		[Route("all")]
		public ActionResult GetAll()
		{
			return Ok(_contactService.GetAllContacts());
		}

		[HttpPost]
		public ActionResult CreateContact(ContactModel contact)
		{
			ModelState.AppendStatefulValidation(ContactValidation.ValidateContactModel(contact));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_contactService.CreateContact(contact));
		}

		[HttpPut]
		public ActionResult UpdateContact(ContactModel contact)
		{
			ModelState.AppendStatefulValidation(ContactValidation.ValidateContactModel(contact));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_contactService.UpdateContact(contact));
		}

		[HttpDelete]
		[Route("delete/{contactId}")]
		public ActionResult DeleteContact(Guid contactId)
		{
			return Ok(_contactService.DeleteContact(contactId));
		}
	}
}
