
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddRoleRepositoryDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}