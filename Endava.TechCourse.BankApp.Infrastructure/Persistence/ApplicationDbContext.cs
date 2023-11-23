﻿using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Wallet> Wallets { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		public DbSet<WalletType> WalletTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Wallet>().HasKey(w => w.Id);
			modelBuilder.Entity<Currency>().HasKey(c => c.Id);
			modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
			modelBuilder.Entity<WalletType>().HasKey(wt => wt.Id);

			modelBuilder.Entity<Wallet>()
				.HasIndex(w => w.WalletCode)
				.IsUnique();

			modelBuilder.Entity<Currency>()
				.HasMany(c => c.Wallets)
				.WithOne(e => e.Currency)
				.HasForeignKey(c => c.CurrencyId)
				.IsRequired();

			modelBuilder.ApplyConfiguration(new RoleConfigurations());

			base.OnModelCreating(modelBuilder);
		}
	}
}