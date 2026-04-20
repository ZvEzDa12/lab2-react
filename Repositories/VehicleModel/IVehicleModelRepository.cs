
namespace Global;
public interface IVehicleModelRepository
{
    public Task<VehicleModelListRepositoryDto> GetAllAsync(VehicleModelQueryRepositoryDto queryDto);

    public Task<VehicleModelRepositoryDto> GetByIdAsync(int id);

    public Task<VehicleModelRepositoryDto> AddAsync(AddVehicleModelRepositoryDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateVehicleModelRepositoryDto updateDto);
}