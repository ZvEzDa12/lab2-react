
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Global;
public class AppDbContext : IdentityDbContext<IdentityUser>
{

public DbSet<Color> ColorList { get; set; }

	
public DbSet<FuelType> FuelTypeList { get; set; }

	
public DbSet<Insurance> InsuranceList { get; set; }

	
public DbSet<Maintenance> MaintenanceList { get; set; }

	
public DbSet<MaintenanceType> MaintenanceTypeList { get; set; }

	
public DbSet<Manufacturer> ManufacturerList { get; set; }

	
public DbSet<OwnershipHistory> OwnershipHistoryList { get; set; }

	
public DbSet<Role> RoleList { get; set; }

	
public DbSet<User> UserList { get; set; }

	
public DbSet<Vehicle> VehicleList { get; set; }

	
public DbSet<VehicleCategory> VehicleCategoryList { get; set; }

	
public DbSet<VehicleModel> VehicleModelList { get; set; }

    public AppDbContext(){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=kursa4;Username=postgres;Password=1234");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ColorMap())
	.ApplyConfiguration(new FuelTypeMap())
	.ApplyConfiguration(new InsuranceMap())
	.ApplyConfiguration(new MaintenanceMap())
	.ApplyConfiguration(new MaintenanceTypeMap())
	.ApplyConfiguration(new ManufacturerMap())
	.ApplyConfiguration(new OwnershipHistoryMap())
	.ApplyConfiguration(new RoleMap())
	.ApplyConfiguration(new UserMap())
	.ApplyConfiguration(new VehicleMap())
	.ApplyConfiguration(new VehicleCategoryMap())
	.ApplyConfiguration(new VehicleModelMap());
        Role r = new Role { 
            
            Id = 1,
            Name = "admin" 
        };
            builder.Entity<Role>().HasData(r);
            builder.Entity<User>().HasData(
                new User {
                    
                    Login = "admin",
                    PasswordHash = Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes("admin"))).ToLower(),
                    Id = 1,
                    RoleId = 1
                });
        base.OnModelCreating(builder);
    }
}