using System;
using Microsoft.EntityFrameworkCore;
using StorageApi.Entities;
namespace StorageApi.DbContexts;

public class ProductInfoContext : DbContext
{
	public DbSet<Product> Products { get; set; }

	public ProductInfoContext(DbContextOptions<ProductInfoContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>().HasData(
			new Product("Keyboard", "Electronics", 120) { Id = 1, Description = "Best keyboard out there, get it now!", Count = 1 },
			new Product("Salami", "Food", 3) { Id = 2 },
			new Product("Frozen Pizza", "Frozen Food", 5) { Id = 3 }
		);


		base.OnModelCreating(modelBuilder);
	}
}
