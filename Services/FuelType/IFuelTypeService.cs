
namespace Global;
public interface IFuelTypeService
{
    public Task<FuelTypeListServiceDto> GetAllAsync(FuelTypeQueryServiceDto queryDto);

    public Task<FuelTypeServiceDto> GetByIdAsync(short id);

    public Task<FuelTypeServiceDto> AddAsync(AddFuelTypeServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateFuelTypeServiceDto updateDto);
}