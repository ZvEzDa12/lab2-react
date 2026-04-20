using System.ComponentModel.DataAnnotations;
public class VehicleModel{
    [Required]
	public int Id { get;set; }
	[StringLength(40)]
	public string Name { get;set; }
	public long ManufacturerId { get;set; }
	public Manufacturer Manufacturer {get;set;}
	public short VehicleCategoryId { get;set; }
	public VehicleCategory VehicleCategory {get;set;}
	public short Power { get;set; }
	public short FuelTypeId { get;set; }
	public FuelType FuelType {get;set;}
	public int LoadCapacity { get;set; }
	public decimal? Co2Emissions { get;set; }
	public ICollection<Vehicle> Vehicles { get;set; }
}