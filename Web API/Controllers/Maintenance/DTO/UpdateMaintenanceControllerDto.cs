
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateMaintenanceControllerDto
{
    [Required]
	public long Id { get; set; }
	public long? VehicleId { get; set; }
	public int? MaintenanceTypeId { get; set; }
	public DateTime? ServiceDate { get; set; }
	public decimal? Cost { get; set; }
	[StringLength(100)]
	public string? Description { get; set; }
}