using System;
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
			return Ok(_contactService.GetContact(contactId));
		}

		[HttpPost]
		public ActionResult CreateContact(ContactModel contact)
		{
			ModelState.AppendStatefulValidation(ContactValidation.ValidateContactModel(contact));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_contactService.CreateContact(contact));
		}
	}
}
