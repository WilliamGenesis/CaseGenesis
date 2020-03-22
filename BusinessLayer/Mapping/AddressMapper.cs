using Entities;
using Models;
using System.Linq;

namespace BusinessLayer.Mapping
{
	public static class AddressMapper
	{
		public static ContactAddress ToContactAddress(this AddressModel model, ContactModel contact)
		{
			return new ContactAddress
			{
				Id = model.Id,
				Address = model.Address,
				PostalCode = model.PostalCode,
				Country = model.Country,
				ContactId = contact.Id
			};
		}

		public static CompanyAddress ToMainCompanyAddress(this AddressModel model)
		{
			return new CompanyAddress
			{
				Id = model.Id,
				Address = model.Address,
				PostalCode = model.PostalCode,
				Country = model.Country,
				IsMainAddress = true
			};
		}

		public static CompanyAddress ToCompanyAddress(this AddressModel model, CompanyModel company)
		{
			return new CompanyAddress
			{
				Id = model.Id,
				Address = model.Address,
				PostalCode = model.PostalCode,
				Country = model.Country,
				CompanyId = company.Id,
				IsMainAddress = false
			};
		}

		public static CompanyAddress[] ToCompanyAddresses(this AddressModel[] models, CompanyModel company)
		{
			return models.Select(model => model.ToCompanyAddress(company)).ToArray();
		}

		public static AddressModel ToAddressModel(this ContactAddress entity)
		{
			return new AddressModel
			{
				Id = entity.Id,
				Address = entity.Address,
				PostalCode = entity.PostalCode,
				Country = entity.Country
			};
		}

		public static AddressModel ToAddressModel(this CompanyAddress entity)
		{
			return new AddressModel
			{
				Id = entity.Id,
				Address = entity.Address,
				PostalCode = entity.PostalCode,
				Country = entity.Country
			};
		}

		public static AddressModel[] ToAdressModels(this CompanyAddress[] entities)
		{
			return entities.Select(model => model.ToAddressModel()).ToArray();
		}
	}
}
