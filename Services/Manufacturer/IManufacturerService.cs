
namespace Global;
public interface IManufacturerService
{
    public Task<ManufacturerListServiceDto> GetAllAsync(ManufacturerQueryServiceDto queryDto);

    public Task<ManufacturerServiceDto> GetByIdAsync(long id);

    public Task<ManufacturerServiceDto> AddAsync(AddManufacturerServiceDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateManufacturerServiceDto updateDto);
}