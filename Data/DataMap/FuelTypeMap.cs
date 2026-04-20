using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class FuelTypeMap : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        builder.ToTable("fuel_type");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
        builder.HasIndex(v => v.Name).IsUnique();
                    
            
        
    }
}