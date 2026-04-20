
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateMaintenanceTypeControllerDto
{
    [Required]
	public int Id { get; set; }
	[StringLength(100)]
	public string? Name { get; set; }
}