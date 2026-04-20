
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddManufacturerServiceDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	[StringLength(100)]
	public string Name { get; set; }
	[StringLength(50)]
	public string? Country { get; set; }
	public short? FoundedYear { get; set; }
}