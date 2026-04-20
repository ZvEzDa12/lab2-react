
namespace Global;
public class MaintenanceServiceDto
{
    public long Id { get; set; }
	public long VehicleId { get; set; }
	public int MaintenanceTypeId { get; set; }
	public DateTime ServiceDate { get; set; }
	public decimal? Cost { get; set; }
	public string? Description { get; set; }
}