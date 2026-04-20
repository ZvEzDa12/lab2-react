using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class InsuranceMap : IEntityTypeConfiguration<Insurance>
{
    public void Configure(EntityTypeBuilder<Insurance> builder)
    {
        builder.ToTable("insurance");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.VehicleId).HasColumnName("vehicle_id").IsRequired();
		builder.Property(d => d.PolicyNumber).HasColumnName("policy_number").HasMaxLength(30).IsRequired();
		builder.Property(d => d.Company).HasColumnName("company").HasMaxLength(100).IsRequired();
		builder.Property(d => d.StartDate).HasColumnName("start_date").IsRequired();
		builder.Property(d => d.EndDate).HasColumnName("end_date").IsRequired();
        
                    
        builder.HasOne(d => d.Vehicle)
                .WithMany(e => e.Insurances)
                .HasForeignKey(d => d.VehicleId);    
        
    }
}