using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class OwnershipHistoryMap : IEntityTypeConfiguration<OwnershipHistory>
{
    public void Configure(EntityTypeBuilder<OwnershipHistory> builder)
    {
        builder.ToTable("ownership_history");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.VehicleId).HasColumnName("vehicle_id").IsRequired();
		builder.Property(d => d.UserId).HasColumnName("user_id").IsRequired();
		builder.Property(d => d.StartDate).HasColumnName("start_date").IsRequired();
		builder.Property(d => d.EndDate).HasColumnName("end_date");
        
                    
        builder.HasOne(d => d.Vehicle)
                .WithMany(e => e.OwnershipHistories)
                .HasForeignKey(d => d.VehicleId);

		builder.HasOne(d => d.User)
                .WithMany(e => e.OwnershipHistories)
                .HasForeignKey(d => d.UserId);    
        
    }
}