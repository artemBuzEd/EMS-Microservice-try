using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfig;

public class UserEventCalendarConfiguration : IEntityTypeConfiguration<UserEventCalendar>
{
    public void Configure(EntityTypeBuilder<UserEventCalendar> builder)
    {
        builder.ToTable("UserEventCalendar");
        builder.ToTable(t =>
            t.HasCheckConstraint("CK_STATUS_OF_EVENT", "status IN ('Registered','Interested','Attended')"));
        builder.HasKey(u => u.id);
        builder.HasIndex(u => u.user_id);
        builder.Property(u => u.user_id).IsRequired().HasMaxLength(128);
        builder.Property(u => u.registration_id);
        builder.Property(u => u.added_at).HasColumnType("DATE").HasDefaultValueSql("CURRENT_DATE").IsRequired();
        builder.Property(u => u.status).IsRequired().HasMaxLength(50);
        
    }
}