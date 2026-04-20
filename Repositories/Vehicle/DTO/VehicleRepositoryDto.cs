
namespace Global;
public class VehicleRepositoryDto
{
    public long Id { get; set; }
	public int ModelId { get; set; }
	public int? ColorId { get; set; }
	public string Vin { get; set; }
	public short ProductionYear { get; set; }
	public int Mileage { get; set; }
	public string? RegistrationNumber { get; set; }
}