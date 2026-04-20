using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class MaintenanceTypeMap : IEntityTypeConfiguration<MaintenanceType>
{
    public void Configure(EntityTypeBuilder<MaintenanceType> builder)
    {
        builder.ToTable("maintenance_type");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.HasIndex(v => v.Name).IsUnique();
                    
            
        
    }
}