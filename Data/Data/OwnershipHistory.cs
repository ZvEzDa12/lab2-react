using System.ComponentModel.DataAnnotations;
public class OwnershipHistory{
    [Required]
	public long Id { get;set; }
	public long VehicleId { get;set; }
	public Vehicle Vehicle {get;set;}
	public long UserId { get;set; }
	public User User {get;set;}
	public DateTime StartDate { get;set; }
	public DateTime? EndDate { get;set; }
}