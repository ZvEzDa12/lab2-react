
namespace Global;
public interface IManufacturerRepository
{
    public Task<ManufacturerListRepositoryDto> GetAllAsync(ManufacturerQueryRepositoryDto queryDto);

    public Task<ManufacturerRepositoryDto> GetByIdAsync(long id);

    public Task<ManufacturerRepositoryDto> AddAsync(AddManufacturerRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateManufacturerRepositoryDto updateDto);
}