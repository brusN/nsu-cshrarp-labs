using lab4_db.model;
using Microsoft.EntityFrameworkCore;

namespace lab2;

public class DataContext: DbContext
{
    public DbSet<PrincessAttemptEntity> Attempts { get; set; }
    public DbSet<Contender> Contenders { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<PrincessAttemptEntity>()
            .HasMany(e => e.Сontenders)
            .WithOne(e=> e.PrincessAttemptEntity)
            .HasForeignKey(e=> e.AttractionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder
            .Entity<PrincessAttemptEntity>()
            .HasIndex(e => e.Name)
            .IsUnique();
    }
}