using System;

namespace Models
{
	public class ContactModel
	{
		public Guid Id { get; set; }
		public AddressModel Address { get; set; }
		public CompanyModel[] Companies { get; set; }
		public bool IsFreelance { get; set; }
		public string TvaNumber { get; set; }
	}
}
