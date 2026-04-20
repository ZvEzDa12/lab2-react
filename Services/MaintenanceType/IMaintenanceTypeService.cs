
namespace Global;
public interface IMaintenanceTypeService
{
    public Task<MaintenanceTypeListServiceDto> GetAllAsync(MaintenanceTypeQueryServiceDto queryDto);

    public Task<MaintenanceTypeServiceDto> GetByIdAsync(int id);

    public Task<MaintenanceTypeServiceDto> AddAsync(AddMaintenanceTypeServiceDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateMaintenanceTypeServiceDto updateDto);
}