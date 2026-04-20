
namespace Global;
public interface IMaintenanceTypeRepository
{
    public Task<MaintenanceTypeListRepositoryDto> GetAllAsync(MaintenanceTypeQueryRepositoryDto queryDto);

    public Task<MaintenanceTypeRepositoryDto> GetByIdAsync(int id);

    public Task<MaintenanceTypeRepositoryDto> AddAsync(AddMaintenanceTypeRepositoryDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateMaintenanceTypeRepositoryDto updateDto);
}