using ITT.CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace ITT.CRM.App
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Contact>().HasData();
		}
		public DbSet<Contact> Contacts { get; set; }
	}
}
