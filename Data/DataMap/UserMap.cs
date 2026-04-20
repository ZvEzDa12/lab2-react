using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Login).HasColumnName("login").HasMaxLength(32).IsRequired();
		builder.Property(d => d.PasswordHash).HasColumnName("password_hash").HasMaxLength(32).IsRequired();
		builder.Property(d => d.RoleId).HasColumnName("role_id");
		builder.Property(d => d.FirstName).HasColumnName("first_name").HasMaxLength(50);
		builder.Property(d => d.LastName).HasColumnName("last_name").HasMaxLength(50);
		builder.Property(d => d.MiddleName).HasColumnName("middle_name").HasMaxLength(50);
		builder.Property(d => d.PassportNumber).HasColumnName("passport_number").HasMaxLength(20);
		builder.Property(d => d.Phone).HasColumnName("phone");
        builder.HasIndex(v => v.Login).IsUnique();
		builder.HasIndex(v => v.PassportNumber).IsUnique();
                    
        builder.HasOne(d => d.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(d => d.RoleId);    
        
    }
}