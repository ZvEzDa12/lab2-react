
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateColorRepositoryDto
{
    [Required]
	public int Id { get; set; }
	[StringLength(30)]
	public string? Name { get; set; }
	[StringLength(7)]
	public string? HexCode { get; set; }
}