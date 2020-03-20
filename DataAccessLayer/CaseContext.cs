using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
	public class CaseContext : DbContext
	{
		public CaseContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Contact> Employees { get; set; }
	}
}
