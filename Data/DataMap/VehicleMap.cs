using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class VehicleMap : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicle");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.ModelId).HasColumnName("model_id").IsRequired();
		builder.Property(d => d.ColorId).HasColumnName("color_id");
		builder.Property(d => d.Vin).HasColumnName("vin").HasMaxLength(17).IsRequired();
		builder.Property(d => d.ProductionYear).HasColumnName("production_year").IsRequired();
		builder.Property(d => d.Mileage).HasColumnName("mileage");
		builder.Property(d => d.RegistrationNumber).HasColumnName("registration_number").HasMaxLength(20);
        builder.HasIndex(v => v.Vin).IsUnique();
		builder.HasIndex(v => v.RegistrationNumber).IsUnique();
                    
        builder.HasOne(d => d.VehicleModel)
                .WithMany(e => e.Vehicles)
                .HasForeignKey(d => d.ModelId);

		builder.HasOne(d => d.Color)
                .WithMany(e => e.Vehicles)
                .HasForeignKey(d => d.ColorId);    
        
    }
}