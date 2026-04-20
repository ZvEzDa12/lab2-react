using System.ComponentModel.DataAnnotations;
public class Vehicle{
    [Required]
	public long Id { get;set; }
	public int ModelId { get;set; }
	public VehicleModel VehicleModel {get;set;}
	public int? ColorId { get;set; }
	public Color Color {get;set;}
	[StringLength(17)]
	public string Vin { get;set; }
	public short ProductionYear { get;set; }
	public int Mileage { get;set; }
	[StringLength(20)]
	public string? RegistrationNumber { get;set; }
	public ICollection<OwnershipHistory> OwnershipHistories { get;set; }
	public ICollection<Maintenance> Maintenances { get;set; }
	public ICollection<Insurance> Insurances { get;set; }
}