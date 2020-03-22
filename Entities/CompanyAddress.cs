using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public class CompanyAddress
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		public string Address { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }

		public Guid CompanyId { get; set; }
		public bool IsMainAddress { get; set; }

		public Company Company { get; set; }
	}
}
