using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class ContactModel
	{
		public Guid Id { get; set; }
		[Required]
		public AddressModel Address { get; set; }
		public CompanyModel[] Companies { get; set; }
		[Required]
		public bool IsFreelance { get; set; }
		public string TvaNumber { get; set; }
	}
}
