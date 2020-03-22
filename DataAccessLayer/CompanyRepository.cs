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
			_fakeObjectGenerator.Companies.Add(ResolveInsertCompany(company));

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
			return ResolveCompany(_fakeObjectGenerator.Companies.FirstOrDefault(company => company.Id == companyId));
		}

		public Guid UpdateCompany(Company company)
		{
			var original = _fakeObjectGenerator.Companies.FirstOrDefault(comp => comp.Id == company.Id);

			if (original == null)
				throw new Exception("The contact you are trying to update does not exist");

			_fakeObjectGenerator.Companies.Remove(original);
			_fakeObjectGenerator.Companies.Add(ResolveInsertCompany(company, false));

			return company.Id;
		}

		public Guid UpsertCompany(Company company)
		{
			var original = _fakeObjectGenerator.Companies.FirstOrDefault(comp => comp.Id == company.Id);

			if (original != null)
			{
				_fakeObjectGenerator.Companies.Remove(original);
				_fakeObjectGenerator.Companies.Add(ResolveInsertCompany(company, false));
			}

			_fakeObjectGenerator.Companies.Add(ResolveInsertCompany(company));

			return company.Id;
		}

		#region HelpersToSimulateEF

		public Company ResolveInsertCompany(Company company, bool overrideId = true)
		{
			company.MainAddress = null;
			company.OtherAddresses = null;

			company.Id =  overrideId ? _fakeObjectGenerator.GetNewGuid() : company.Id;

			return company;
		}

		public Company ResolveCompany(Company company)
		{
			if (company is null)
				return null;

			company.MainAddress = _fakeObjectGenerator.CompanyAddresses.FirstOrDefault(address => address.Id == company.MainAddressId);
			company.OtherAddresses = _fakeObjectGenerator.CompanyAddresses.Where(address => address.CompanyId == company.Id && !address.IsMainAddress).ToArray();

			return company;
		}

		#endregion
	}
}
