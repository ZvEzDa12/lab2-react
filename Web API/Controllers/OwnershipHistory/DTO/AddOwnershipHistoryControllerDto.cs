
using System.ComponentModel.DataAnnotations;
namespace Global;

public class AddOwnershipHistoryControllerDto
{
	[Required]
	public long VehicleId { get; set; }
	[Required]
	public long UserId { get; set; }
	[Required]
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}