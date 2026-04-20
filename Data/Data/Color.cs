using System.ComponentModel.DataAnnotations;
public class Color{
    [Required]
	public int Id { get;set; }
	[StringLength(30)]
	public string Name { get;set; }
	[StringLength(7)]
	public string HexCode { get;set; }
	public ICollection<Vehicle> Vehicles { get;set; }
}