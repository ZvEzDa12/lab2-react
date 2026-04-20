
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddOwnershipHistoryServiceDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	public long VehicleId { get; set; }
	[Required]
	public long UserId { get; set; }
	[Required]
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}