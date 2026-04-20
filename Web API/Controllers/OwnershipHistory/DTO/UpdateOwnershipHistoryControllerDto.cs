
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateOwnershipHistoryControllerDto
{
    [Required]
	public long Id { get; set; }
	public long? VehicleId { get; set; }
	public long? UserId { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}