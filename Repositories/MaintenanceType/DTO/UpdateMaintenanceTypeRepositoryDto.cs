
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateMaintenanceTypeRepositoryDto
{
    [Required]
	public int Id { get; set; }
	[StringLength(100)]
	public string? Name { get; set; }
}