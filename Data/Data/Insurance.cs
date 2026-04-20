using System.ComponentModel.DataAnnotations;
public class Insurance{
    [Required]
	public long Id { get;set; }
	public long VehicleId { get;set; }
	public Vehicle Vehicle {get;set;}
	[StringLength(30)]
	public string PolicyNumber { get;set; }
	[StringLength(100)]
	public string Company { get;set; }
	public DateTime StartDate { get;set; }
	public DateTime EndDate { get;set; }
	public decimal Cost { get;set; }
}