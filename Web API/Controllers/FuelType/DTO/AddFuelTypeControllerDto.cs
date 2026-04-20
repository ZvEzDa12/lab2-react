
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddFuelTypeControllerDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
}