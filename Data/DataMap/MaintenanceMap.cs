using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class MaintenanceMap : IEntityTypeConfiguration<Maintenance>
{
    public void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        builder.ToTable("maintenance");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.VehicleId).HasColumnName("vehicle_id").IsRequired();
		builder.Property(d => d.MaintenanceTypeId).HasColumnName("maintenance_type_id").IsRequired();
		builder.Property(d => d.ServiceDate).HasColumnName("service_date").IsRequired();
		builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(100);
        
                    
        builder.HasOne(d => d.Vehicle)
                .WithMany(e => e.Maintenances)
                .HasForeignKey(d => d.VehicleId);

		builder.HasOne(d => d.MaintenanceType)
                .WithMany(e => e.Maintenances)
                .HasForeignKey(d => d.MaintenanceTypeId);    
        
    }
}