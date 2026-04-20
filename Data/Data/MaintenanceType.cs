using System.ComponentModel.DataAnnotations;
public class MaintenanceType{
    [Required]
	public int Id { get;set; }
	[StringLength(100)]
	public string Name { get;set; }
	public ICollection<Maintenance> Maintenances { get;set; }
}