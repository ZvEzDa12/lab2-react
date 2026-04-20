
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddInsuranceServiceDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	public long VehicleId { get; set; }
	[Required]
	[StringLength(30)]
	public string PolicyNumber { get; set; }
	[Required]
	[StringLength(100)]
	public string Company { get; set; }
	[Required]
	public DateTime StartDate { get; set; }
	[Required]
	public DateTime EndDate { get; set; }
	public decimal Cost { get; set; }
}