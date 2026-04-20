
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddColorRepositoryDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
	[Required]
	[StringLength(7)]
	public string HexCode { get; set; }
}