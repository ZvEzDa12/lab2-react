
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateUserRepositoryDto
{

    [Required]
    public long Id { get; set; }

	[StringLength(32)]
	public string? Login { get; set; }
	public short? RoleId { get; set; }
	[StringLength(50)]
	public string? FirstName { get; set; }
	[StringLength(50)]
	public string? LastName { get; set; }
	[StringLength(50)]
	public string? MiddleName { get; set; }
	[StringLength(20)]
	public string? PassportNumber { get; set; }
	public long? Phone { get; set; }
    public string? PasswordHash { get; set; }
}