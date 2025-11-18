using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityConfig;

public class UserProfileDbContext : DbContext
{
    public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;
    public virtual DbSet<UserEventCalendar> UserEventCalendars { get; set; } = null!;
    public virtual DbSet<UserComment> UserComments { get; set; } = null!;

    public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}