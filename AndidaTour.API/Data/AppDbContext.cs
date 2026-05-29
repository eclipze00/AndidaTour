using AndidaTour.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AndidaTour.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ClientEntity> Clients => Set<ClientEntity>();
    public DbSet<FlightQuoteEntity> Quotes => Set<FlightQuoteEntity>();
    public DbSet<PriceAlertEntity> Alerts => Set<PriceAlertEntity>();
    public DbSet<QuoteRequestEntity> QuoteRequests => Set<QuoteRequestEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<UserEntity>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();
        });

        // Client
        modelBuilder.Entity<ClientEntity>(e =>
        {
            e.HasOne(c => c.User)
             .WithMany(u => u.Clients)
             .HasForeignKey(c => c.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // FlightQuote
        modelBuilder.Entity<FlightQuoteEntity>(e =>
        {
            e.Property(q => q.BestPrice).HasColumnType("numeric(18,2)");

            e.HasOne(q => q.Client)
             .WithMany(c => c.Quotes)
             .HasForeignKey(q => q.ClientId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(q => q.User)
             .WithMany(u => u.Quotes)
             .HasForeignKey(q => q.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // PriceAlert
        modelBuilder.Entity<PriceAlertEntity>(e =>
        {
            e.Property(a => a.MaxPrice).HasColumnType("numeric(18,2)");

            e.HasOne(a => a.User)
             .WithMany(u => u.Alerts)
             .HasForeignKey(a => a.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // QuoteRequest
        modelBuilder.Entity<QuoteRequestEntity>(e =>
        {
            e.Property(q => q.AdminPrice).HasColumnType("numeric(18,2)");

            e.HasOne(q => q.ClientUser)
            .WithMany(u => u.QuoteRequests)
            .HasForeignKey(q => q.ClientUserId)
            .OnDelete(DeleteBehavior.Cascade);
        });
    }
}