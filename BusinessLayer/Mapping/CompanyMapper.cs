using Entities;
using Models;
using System;
using System.Linq;

namespace BusinessLayer.Mapping
{
	public static class CompanyMapper
	{
		public static Company ToCompany(this CompanyModel model)
		{
			return new Company
			{
				Id = model.Id,
				MainAddress = model.MainAddress.ToCompanyAddress(model),
				OtherAddresses = model.OtherAddresses?.ToCompanyAddresses(model),
				ContactId = model.ContactId,
				TvaNumber = model.TvaNumber
			};
		}

		public static Company[] ToCompanies(this CompanyModel[] companyModels)
		{
			return companyModels.Select(entity => entity.ToCompany()).ToArray();
		}

		public static CompanyModel ToCompanyModel(this Company entity)
		{
			return new CompanyModel
			{
				Id = entity.Id,
				MainAddress = entity.MainAddress.ToAddressModel(),
				OtherAddresses = entity.OtherAddresses?.ToAdressModels(),
				ContactId = entity.ContactId,
				TvaNumber = entity.TvaNumber
			};
		}

		public static CompanyModel[] ToCompanyModels(this Company[] entities)
		{
			return entities.Select(entity => entity.ToCompanyModel()).ToArray();
		}
	}
}
