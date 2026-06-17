using Microsoft.EntityFrameworkCore;
using MovieApp.Models;
namespace MovieApp.Data;

public class AppDbContext : DbContext
{
	public DbSet<Movie> Movies { get; set; }
	public DbSet<Actor> Actors { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<MovieDetails> MovieDetails { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<User> Users { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var movie = modelBuilder.Entity<Movie>();
		movie.Property(m => m.Title).HasMaxLength(100);
		movie.ToTable(t =>
		{
			t.HasCheckConstraint(
				"CK_Movie_Year",
				"Year >= 1800 AND Year <= 2026"
			);
		});


		modelBuilder.Entity<Actor>().Property(a => a.Name).HasMaxLength(100);
		modelBuilder.Entity<Genre>().Property(g => g.Name).HasMaxLength(30);

		var movieDetails = modelBuilder.Entity<MovieDetails>();
		movieDetails.Property(md => md.Synopsis).HasMaxLength(500);
		movieDetails.Property(md => md.Language).HasMaxLength(50);

		var review = modelBuilder.Entity<Review>();
		review.Property(r => r.Comment).HasMaxLength(250);
		review.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		review.Property(r => r.Edited).HasDefaultValue(false);
		review.ToTable(t =>
		{
			t.HasCheckConstraint(
				"CK_Review_Rating",
				"Rating >= 1 AND Rating <= 5"
			);
		});

		var user = modelBuilder.Entity<User>();
		user.Property(u => u.Name).HasMaxLength(50);
		user.Property(u => u.IsAHater).HasDefaultValue(false);

		base.OnModelCreating(modelBuilder);
	}
}