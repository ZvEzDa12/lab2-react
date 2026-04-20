
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddVehicleCategoryRepositoryDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}