
namespace Global;
public interface IMaintenanceRepository
{
    public Task<MaintenanceListRepositoryDto> GetAllAsync(MaintenanceQueryRepositoryDto queryDto);

    public Task<MaintenanceRepositoryDto> GetByIdAsync(long id);

    public Task<MaintenanceRepositoryDto> AddAsync(AddMaintenanceRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateMaintenanceRepositoryDto updateDto);
}