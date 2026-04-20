
namespace Global;
public interface IVehicleService
{
    public Task<VehicleListServiceDto> GetAllAsync(VehicleQueryServiceDto queryDto);

    public Task<VehicleServiceDto> GetByIdAsync(long id);

    public Task<VehicleServiceDto> AddAsync(AddVehicleServiceDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateVehicleServiceDto updateDto);
}