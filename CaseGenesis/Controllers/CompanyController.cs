using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;

namespace CaseGenesis.Controllers
{
	[Route("api/company")]
	[ApiController]
    public class CompanyController : Controller
    {
		private readonly ICompanyService _companyService;

		public CompanyController(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		[HttpGet]
		[Route("{companyId}")]
		public ActionResult Get(Guid companyId)
		{
			var company = _companyService.GetCompany(companyId);

			if (company is null)
				return NotFound();

			return Ok(_companyService.GetCompany(companyId));
		}

		[HttpPost]
		public ActionResult CreateCompany(CompanyModel company)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_companyService.CreateCompany(company));
		}

		[HttpPut]
		public ActionResult UpdateCompany(CompanyModel company)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_companyService.UpdateCompany(company));
		}

		[HttpDelete]
		[Route("delete/{companyId}")]
		public ActionResult DeleteCompany(Guid companyId)
		{
			if (!_companyService.IsValidForDeletion(companyId))
				return BadRequest("Impossible to delete this company : a contact must always have a company");

			return Ok(_companyService.DeleteCompany(companyId));
		}
	}
}