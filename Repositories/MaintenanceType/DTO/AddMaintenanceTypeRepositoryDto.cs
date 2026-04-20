
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddMaintenanceTypeRepositoryDto
{
	[Required]
	[StringLength(100)]
	public string Name { get; set; }
}