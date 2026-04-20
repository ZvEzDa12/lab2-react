
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddColorControllerDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
	[Required]
	[StringLength(7)]
	public string HexCode { get; set; }
}