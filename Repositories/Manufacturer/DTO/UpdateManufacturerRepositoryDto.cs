
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateManufacturerRepositoryDto
{
    [Required]
	public long Id { get; set; }
	[StringLength(100)]
	public string? Name { get; set; }
	[StringLength(50)]
	public string? Country { get; set; }
	public short? FoundedYear { get; set; }
}