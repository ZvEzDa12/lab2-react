using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class VehicleCategoryMap : IEntityTypeConfiguration<VehicleCategory>
{
    public void Configure(EntityTypeBuilder<VehicleCategory> builder)
    {
        builder.ToTable("vehicle_category");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.HasIndex(v => v.Name).IsUnique();
                    
            
        
    }
}