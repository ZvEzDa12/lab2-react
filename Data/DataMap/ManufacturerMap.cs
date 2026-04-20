using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class ManufacturerMap : IEntityTypeConfiguration<Manufacturer>
{
    public void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        builder.ToTable("manufacturer");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
		builder.Property(d => d.Country).HasColumnName("country").HasMaxLength(50);
		builder.Property(d => d.FoundedYear).HasColumnName("founded_year");
        
                    
            
        
    }
}