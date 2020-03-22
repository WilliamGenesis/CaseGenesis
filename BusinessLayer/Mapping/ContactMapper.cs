using Entities;
using Models;
using System.Linq;

namespace BusinessLayer.Mapping
{
	public static class ContactMapper
	{
		public static Contact ToContact (this ContactModel model)
		{
			return new Contact
			{
				Id = model.Id,
				Address = model.Address.ToContactAddress(model),
				IsFreelance = model.IsFreelance,
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

		public static ContactModel[] ToContactModels(this Contact[] contacts)
		{
			return contacts.Select(contact => contact.ToContactModel()).ToArray();
		}
	}
}
