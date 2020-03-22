using Entities;
using Models;
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
				OtherAddresses = model.OtherAddresses.ToCompanyAddresses(model),
				TvaNumber = model.TvaNumber
			};
		}

		public static CompanyModel ToCompanyModel(this Company entity)
		{
			return new CompanyModel
			{
				Id = entity.Id,
				MainAddress = entity.MainAddress.ToAddressModel(),
				OtherAddresses = entity.OtherAddresses.ToAdressModels(),
				TvaNumber = entity.TvaNumber
			};
		}

		public static CompanyModel[] ToCompanyModels(this Company[] entities)
		{
			return entities.Select(entity => entity.ToCompanyModel()).ToArray();
		}
	}
}
