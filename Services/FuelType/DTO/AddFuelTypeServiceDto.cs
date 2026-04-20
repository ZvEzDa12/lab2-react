
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddFuelTypeServiceDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
}