using System;

namespace Models
{
	public class CompanyModel
	{
		public Guid Id { get; set; }
		public AddressModel MainAddress { get; set; }
		public AddressModel[] OtherAddresses { get; set; }
		public string TvaNumber { get; set; }
	}
}
