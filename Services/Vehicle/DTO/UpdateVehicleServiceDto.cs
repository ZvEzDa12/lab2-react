
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateVehicleServiceDto
{
    [Required]
	public long Id { get; set; }
	public int? ModelId { get; set; }
	public int? ColorId { get; set; }
	[StringLength(17)]
	public string? Vin { get; set; }
	public short? ProductionYear { get; set; }
	public int? Mileage { get; set; }
	[StringLength(20)]
	public string? RegistrationNumber { get; set; }
}