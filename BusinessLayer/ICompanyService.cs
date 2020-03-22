using Models;
using System;

namespace BusinessLayer
{
	public interface ICompanyService
	{
		Guid CreateCompany(CompanyModel companyModel);
		Guid UpdateCompany(CompanyModel companyModel);
		Guid UpsertCompany(CompanyModel companyModel);
		bool DeleteCompany(Guid companyId);
		CompanyModel GetCompany(Guid companyId);
		bool IsValidForDeletion(Guid companyId);
	}
}
