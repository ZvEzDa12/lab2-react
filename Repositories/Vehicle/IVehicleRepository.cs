
namespace Global;
public interface IVehicleRepository
{
    public Task<VehicleListRepositoryDto> GetAllAsync(VehicleQueryRepositoryDto queryDto);

    public Task<VehicleRepositoryDto> GetByIdAsync(long id);

    public Task<VehicleRepositoryDto> AddAsync(AddVehicleRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateVehicleRepositoryDto updateDto);
}