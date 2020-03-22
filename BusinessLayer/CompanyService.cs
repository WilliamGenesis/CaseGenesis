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
			var mainAddressId = _addressRepository.UpsertCompanyAddress(companyModel.MainAddress.ToMainCompanyAddress());
			var company = companyModel.ToCompany();
			company.MainAddressId = mainAddressId;

			companyModel.Id = _companyRepository.CreateCompany(company);

			UpsertAddresses(companyModel.OtherAddresses?.ToCompanyAddresses(companyModel));

			return companyModel.Id;
		}

		public bool DeleteCompany(Guid companyId)
		{
			return _companyRepository.DeleteCompany(companyId);
		}

		public CompanyModel GetCompany(Guid companyId)
		{
			return _companyRepository.GetCompany(companyId)?.ToCompanyModel();
		}

		public Guid UpdateCompany(CompanyModel companyModel)
		{
			var mainAddressId = _addressRepository.UpsertCompanyAddress(companyModel.MainAddress.ToMainCompanyAddress());
			var company = companyModel.ToCompany();
			company.MainAddressId = mainAddressId;

			companyModel.Id = _companyRepository.UpdateCompany(company);

			UpsertAddresses(companyModel.OtherAddresses?.ToCompanyAddresses(companyModel));

			return companyModel.Id;
		}

		public Guid UpsertCompany(CompanyModel companyModel)
		{
			var mainAddressId = _addressRepository.UpsertCompanyAddress(companyModel.MainAddress.ToMainCompanyAddress());
			var company = companyModel.ToCompany();
			company.MainAddressId = mainAddressId;

			if (_companyRepository.GetCompany(company.Id) == null)
				companyModel.Id = _companyRepository.CreateCompany(company);
			else
				companyModel.Id = _companyRepository.UpdateCompany(company);

			UpsertAddresses(companyModel.OtherAddresses?.ToCompanyAddresses(companyModel));

			return companyModel.Id;
		}

		private void UpsertAddresses(CompanyAddress[] addresses)
		{
			if (addresses is null)
				return;

			foreach(var address in addresses)
			{
				_addressRepository.UpsertCompanyAddress(address);
			}
		}
	}
}
