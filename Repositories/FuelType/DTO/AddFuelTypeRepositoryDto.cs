
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddFuelTypeRepositoryDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
}