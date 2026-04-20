
namespace Global;
public interface IVehicleModelService
{
    public Task<VehicleModelListServiceDto> GetAllAsync(VehicleModelQueryServiceDto queryDto);

    public Task<VehicleModelServiceDto> GetByIdAsync(int id);

    public Task<VehicleModelServiceDto> AddAsync(AddVehicleModelServiceDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateVehicleModelServiceDto updateDto);
}