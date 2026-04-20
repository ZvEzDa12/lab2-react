
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddVehicleCategoryServiceDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}