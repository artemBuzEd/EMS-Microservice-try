using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfig;

public class UserCommentConfiguration : IEntityTypeConfiguration<UserComment> 
{
    public void Configure(EntityTypeBuilder<UserComment> builder)
    {
        builder.ToTable("UserComment");
        builder.HasKey(u => u.id);
        builder.ToTable(t => t.HasCheckConstraint("CK_USER_RATING", "rating >= 0 AND rating <= 5"));
        builder.HasIndex(u => u.user_id);
        builder.HasIndex(u => u.event_id);
        builder.Property(u => u.event_id).IsRequired();
        builder.Property(u => u.user_id).IsRequired();
        builder.Property(u => u.comment).IsRequired().HasMaxLength(500);
        builder.Property(u => u.added_at).HasColumnType("DATE");
        builder.Property(u => u.is_changed).HasDefaultValue(false);
    }
}