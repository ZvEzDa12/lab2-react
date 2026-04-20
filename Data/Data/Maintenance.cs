using System.ComponentModel.DataAnnotations;
public class Maintenance{
    [Required]
	public long Id { get;set; }
	public long VehicleId { get;set; }
	public Vehicle Vehicle {get;set;}
	public int MaintenanceTypeId { get;set; }
	public MaintenanceType MaintenanceType {get;set;}
	public DateTime ServiceDate { get;set; }
	public decimal? Cost { get;set; }
	[StringLength(100)]
	public string? Description { get;set; }
}