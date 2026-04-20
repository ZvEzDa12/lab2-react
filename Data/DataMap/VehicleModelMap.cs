using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class VehicleModelMap : IEntityTypeConfiguration<VehicleModel>
{
    public void Configure(EntityTypeBuilder<VehicleModel> builder)
    {
        builder.ToTable("vehicle_model");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(40).IsRequired();
		builder.Property(d => d.ManufacturerId).HasColumnName("manufacturer_id").IsRequired();
		builder.Property(d => d.VehicleCategoryId).HasColumnName("vehicle_category_id").IsRequired();
		builder.Property(d => d.Power).HasColumnName("power").IsRequired();
		builder.Property(d => d.FuelTypeId).HasColumnName("fuel_type_id").IsRequired();
		builder.Property(d => d.LoadCapacity).HasColumnName("load_capacity").IsRequired();
        
                    
        builder.HasOne(d => d.Manufacturer)
                .WithMany(e => e.VehicleModels)
                .HasForeignKey(d => d.ManufacturerId);

		builder.HasOne(d => d.VehicleCategory)
                .WithMany(e => e.VehicleModels)
                .HasForeignKey(d => d.VehicleCategoryId);

		builder.HasOne(d => d.FuelType)
                .WithMany(e => e.VehicleModels)
                .HasForeignKey(d => d.FuelTypeId);    
        
    }
}