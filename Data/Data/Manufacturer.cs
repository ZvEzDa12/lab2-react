using System.ComponentModel.DataAnnotations;
public class Manufacturer{
    [Required]
	public long Id { get;set; }
	[StringLength(100)]
	public string Name { get;set; }
	[StringLength(50)]
	public string? Country { get;set; }
	public short? FoundedYear { get;set; }
	public ICollection<VehicleModel> VehicleModels { get;set; }
}