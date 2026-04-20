
namespace Global;
public class InsuranceServiceDto
{
    public long Id { get; set; }
	public long VehicleId { get; set; }
	public string PolicyNumber { get; set; }
	public string Company { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public decimal Cost { get; set; }
}