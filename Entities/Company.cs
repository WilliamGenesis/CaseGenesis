using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public class Company
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		public string TvaNumber { get; set; }
		public CompanyAddress MainAddress { get; set; }

		public CompanyAddress[] OtherAddresses { get; set; }
	}
}
