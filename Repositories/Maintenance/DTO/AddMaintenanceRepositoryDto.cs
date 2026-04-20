
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddMaintenanceRepositoryDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	public long VehicleId { get; set; }
	[Required]
	public int MaintenanceTypeId { get; set; }
	[Required]
	public DateTime ServiceDate { get; set; }
	public decimal? Cost { get; set; }
	[StringLength(100)]
	public string? Description { get; set; }
}