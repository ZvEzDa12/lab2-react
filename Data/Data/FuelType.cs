using System.ComponentModel.DataAnnotations;
public class FuelType{
    [Required]
	public short Id { get;set; }
	[StringLength(30)]
	public string Name { get;set; }
	public ICollection<VehicleModel> VehicleModels { get;set; }
}