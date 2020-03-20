using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class AddressModel
	{
		public Guid Id { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string PostalCode { get; set; }
		[Required]
		public string Country { get; set; }
	}
}
