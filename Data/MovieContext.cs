using Microsoft.EntityFrameworkCore;
using MovieCardsAPI.Models.Entities;

namespace MovieCardsAPI.Models;

public class MovieContext : DbContext {
    public MovieContext(DbContextOptions<MovieContext> options)
        : base(options) {
    }

    public DbSet<Movie> Movies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Director>().HasOne(a => a.ContactInformation)
        .WithOne(b => b.Director)
        .HasForeignKey<ContactInformation>(b => b.Id);
    }


}