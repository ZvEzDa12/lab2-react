
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddMaintenanceTypeControllerDto
{
	[Required]
	[StringLength(100)]
	public string Name { get; set; }
}