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

		public DbSet<Contact> Contact { get; set; }
		public DbSet<Company> Company { get; set; }
		public DbSet<ContactAddress> ContactAddress { get; set; }
		public DbSet<CompanyAddress> CompanyAddress { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Contact>( entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id)
				.HasDefaultValueSql("NEWID()");

				entity.HasOne(contact => contact.Address)
				.WithOne(address => address.Contact)
				.HasForeignKey<ContactAddress>(address => address.ContactId);

				entity.HasMany(contact => contact.Companies)
				.WithOne(company => company.Contact)
				.HasForeignKey(company => company.ContactId);
			});

			modelBuilder.Entity<Company>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id)
				.HasDefaultValueSql("NEWID()");

				entity.HasOne(company => company.MainAddress)
				.WithOne(address => address.Company)
				.HasForeignKey<Company>(company => company.MainAddressId);

				entity.HasMany(company => company.OtherAddresses)
				.WithOne(address => address.Company)
				.HasForeignKey(address => address.CompanyId);
			});

			var fakeDataGenerator = new FakeDataGenerator();

			modelBuilder.Entity<Contact>().HasData(fakeDataGenerator.Contacts);
			modelBuilder.Entity<Company>().HasData(fakeDataGenerator.Companies);
			modelBuilder.Entity<ContactAddress>().HasData(fakeDataGenerator.ContactAddresses);
			modelBuilder.Entity<CompanyAddress>().HasData(fakeDataGenerator.CompanyAddresses);
		}
	}
}
