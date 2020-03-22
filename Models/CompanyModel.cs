using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class CompanyModel
	{
		public Guid Id { get; set; }
		[Required]
		public AddressModel MainAddress { get; set; }
		public AddressModel[] OtherAddresses { get; set; }
		[Required]
		public string TvaNumber { get; set; }
		[Required]
		public Guid ContactId { get; set; }
	}
}
