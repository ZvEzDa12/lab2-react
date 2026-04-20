
namespace Global;
public class UserRepositoryDto
{
    public long Id { get; set; }
	public string Login { get; set; }
	public short? RoleId { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? MiddleName { get; set; }
	public string? PassportNumber { get; set; }
	public long? Phone { get; set; }
}