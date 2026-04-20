
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddVehicleServiceDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	public int ModelId { get; set; }
	public int? ColorId { get; set; }
	[Required]
	[StringLength(17)]
	public string Vin { get; set; }
	[Required]
	public short ProductionYear { get; set; }
	public int Mileage { get; set; }
	[StringLength(20)]
	public string? RegistrationNumber { get; set; }
}