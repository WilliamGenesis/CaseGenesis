using Entities;
using System;

namespace DataAccessLayer
{
	public interface ICompanyRepository
	{
		Guid CreateCompany(Company company);
		Guid UpdateCompany(Company company);
		bool DeleteCompany(Guid companyId);
		Guid UpsertCompany(Company company);
		Company GetCompany(Guid companyId);
	}
}
