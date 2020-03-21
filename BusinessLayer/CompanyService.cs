using System;
using BusinessLayer.Mapping;
using DataAccessLayer;
using Entities;
using Models;

namespace BusinessLayer
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly IAddressRepository _addressRepository;

		public CompanyService(ICompanyRepository companyRepository, IAddressRepository addressRepository)
		{
			_companyRepository = companyRepository;
			_addressRepository = addressRepository;
		}

		public Guid CreateCompany(CompanyModel companyModel)
		{
			var company = companyModel.ToCompany();

			_addressRepository.UpsertCompanyAddress(company.MainAddress);
			UpsertAddresses(company.OtherAddresses);

			return _companyRepository.CreateCompany(company);
		}

		public bool DeleteCompany(Guid companyId)
		{
			return _companyRepository.DeleteCompany(companyId);
		}

		public CompanyModel GetCompany(Guid companyId)
		{
			return _companyRepository.GetCompany(companyId).ToCompanyModel();
		}

		public Guid UpdateCompany(CompanyModel companyModel)
		{
			var company = companyModel.ToCompany();

			_addressRepository.UpsertCompanyAddress(company.MainAddress);
			UpsertAddresses(company.OtherAddresses);

			return _companyRepository.CreateCompany(company);
		}

		public Guid UpsertCompany(CompanyModel companyModel)
		{
			var company = companyModel.ToCompany();

			_addressRepository.UpsertCompanyAddress(company.MainAddress);
			UpsertAddresses(company.OtherAddresses);

			if (_companyRepository.GetCompany(company.Id) == null)
				return _companyRepository.CreateCompany(company);

			return _companyRepository.UpdateCompany(company);
		}

		private void UpsertAddresses(CompanyAddress[] addresses)
		{
			foreach(var address in addresses)
			{
				_addressRepository.UpsertCompanyAddress(address);
			}
		}
	}
}
