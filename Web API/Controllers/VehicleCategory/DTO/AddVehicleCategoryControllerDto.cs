
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddVehicleCategoryControllerDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}