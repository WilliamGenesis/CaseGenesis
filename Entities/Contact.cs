using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public class Contact
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		public bool IsFreelance { get; set; }
		public string TvaNumber { get; set; }
		public Guid ContactAddressId { get; set; }

		public ContactAddress Address { get; set; }
		public Company[] Companies { get; set; }
	}
}
