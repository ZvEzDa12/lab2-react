
namespace Global;
public interface IMaintenanceService
{
    public Task<MaintenanceListServiceDto> GetAllAsync(MaintenanceQueryServiceDto queryDto);

    public Task<MaintenanceServiceDto> GetByIdAsync(long id);

    public Task<MaintenanceServiceDto> AddAsync(AddMaintenanceServiceDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateMaintenanceServiceDto updateDto);
}