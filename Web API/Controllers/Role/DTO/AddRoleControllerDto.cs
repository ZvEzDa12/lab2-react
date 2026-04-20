
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddRoleControllerDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}