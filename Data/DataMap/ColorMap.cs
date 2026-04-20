using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class ColorMap : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("color");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
		builder.Property(d => d.HexCode).HasColumnName("hex_code").HasMaxLength(7).IsRequired();
        builder.HasIndex(v => v.Name).IsUnique();
                    
            
        
    }
}