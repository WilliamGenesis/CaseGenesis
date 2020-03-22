using System;
using System.Linq;
using Entities;

namespace DataAccessLayer
{
	public class CompanyRepository : ICompanyRepository
	{
		IFakeObjectGenerator _fakeObjectGenerator;

		public CompanyRepository(IFakeObjectGenerator fakeObjectGenerator)
		{
			_fakeObjectGenerator = fakeObjectGenerator;
		}

		public Guid CreateCompany(Company company)
		{
			company.Id = _fakeObjectGenerator.GetNewGuid();

			_fakeObjectGenerator.Companies.Add(company);

			return company.Id;
		}

		public bool DeleteCompany(Guid companyId)
		{
			var original = _fakeObjectGenerator.Companies.FirstOrDefault(comp => comp.Id == companyId);

			if (original == null)
				return false;

			_fakeObjectGenerator.Companies.Remove(original);
			return true;
		}

		public Company GetCompany(Guid companyId)
		{
			return _fakeObjectGenerator.Companies.FirstOrDefault(company => company.Id == companyId);
		}

		public Guid UpdateCompany(Company company)
		{
			var original = _fakeObjectGenerator.Companies.FirstOrDefault(comp => comp.Id == company.Id);

			if (original == null)
				throw new Exception("The contact you are trying to update does not exist");

			_fakeObjectGenerator.Companies.Remove(original);
			_fakeObjectGenerator.Companies.Add(company);

			return company.Id;
		}

		public Guid UpsertCompany(Company company)
		{
			var original = _fakeObjectGenerator.Companies.FirstOrDefault(comp => comp.Id == company.Id);

			if (original != null)
				original = company;

			company.Id = _fakeObjectGenerator.GetNewGuid();
			_fakeObjectGenerator.Companies.Add(company);

			return company.Id;
		}
	}
}
