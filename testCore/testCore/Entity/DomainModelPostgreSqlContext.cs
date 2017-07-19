using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using testCore.Entity.Models;

namespace testCore.Entity
{
	public class DomainModelPostgreSqlContext : DbContext
	{
		//public DomainModelPostgreSqlContext(DbContextOptions<DomainModelPostgreSqlContext> options) : base(options)
		//{
		//}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;");
			optionsBuilder.UseNpgsql(@"User ID = ruser; Password = raikou; Host = 192.168.52.128; Port = 5432; Database = test; Pooling = true;");
		}


		public DbSet<DataEventRecord> DataEventRecords { get; set; }

		public DbSet<SourceInfo> SourceInfos { get; set; }

		public DbSet<person> person { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
			builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);
			builder.Entity<person>().HasKey(m => m.id);

			// shadow properties
			builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
			builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");

			base.OnModelCreating(builder);
		}

		public override int SaveChanges()
		{
			ChangeTracker.DetectChanges();

			updateUpdatedProperty<SourceInfo>();
			updateUpdatedProperty<DataEventRecord>();

			return base.SaveChanges();
		}

		private void updateUpdatedProperty<T>() where T : class
		{
			var modifiedSourceInfo =
				ChangeTracker.Entries<T>()
					.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

			foreach (var entry in modifiedSourceInfo)
			{
				entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
			}
		}
	}
}
