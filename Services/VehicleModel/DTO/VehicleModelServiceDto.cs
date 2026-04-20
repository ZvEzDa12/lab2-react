
namespace Global;
public class VehicleModelServiceDto
{
    public int Id { get; set; }
	public string Name { get; set; }
	public long ManufacturerId { get; set; }
	public short VehicleCategoryId { get; set; }
	public short Power { get; set; }
	public short FuelTypeId { get; set; }
	public int LoadCapacity { get; set; }
	public decimal? Co2Emissions { get; set; }
}