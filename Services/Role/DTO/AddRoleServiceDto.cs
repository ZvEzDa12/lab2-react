
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddRoleServiceDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}