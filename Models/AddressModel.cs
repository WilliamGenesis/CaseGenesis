using System;

namespace Models
{
	public class AddressModel
	{
		public Guid Id { get; set; }
		public string Address { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
	}
}
