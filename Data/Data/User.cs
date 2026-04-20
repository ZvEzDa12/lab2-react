using System.ComponentModel.DataAnnotations;
public class User{
    [Required]
	public long Id { get;set; }
	[StringLength(32)]
	public string Login { get;set; }
	[StringLength(32)]
	public string PasswordHash { get;set; }
	public short? RoleId { get;set; }
	public Role Role {get;set;}
	[StringLength(50)]
	public string? FirstName { get;set; }
	[StringLength(50)]
	public string? LastName { get;set; }
	[StringLength(50)]
	public string? MiddleName { get;set; }
	[StringLength(20)]
	public string? PassportNumber { get;set; }
	public long? Phone { get;set; }
	public ICollection<OwnershipHistory> OwnershipHistories { get;set; }
}