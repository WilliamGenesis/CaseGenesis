using Entities;
using Models;

namespace BusinessLayer.Mapping
{
	public static class CompanyMapper
	{
		public static Company ToContact(this CompanyModel model)
		{
			return new Company
			{
				Id = model.Id,
				MainAddress = model.MainAddress.ToCompanyAddress(model),
				OtherAddresses = model.OtherAddresses.ToCompanyAddresses(model),
				TvaNumber = model.TvaNumber
			};
		}

		public static ContactModel ToContactModel(this Contact entity)
		{
			return new ContactModel
			{
				Id = entity.Id,
				Address = entity.Address.ToAddressModel(),
				IsFreelance = entity.IsFreelance,
				TvaNumber = entity.TvaNumber
			};
		}
	}
}
