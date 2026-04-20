
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddMaintenanceTypeServiceDto
{
	[Required]
	[StringLength(100)]
	public string Name { get; set; }
}