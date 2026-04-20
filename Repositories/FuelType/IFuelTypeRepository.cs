
namespace Global;
public interface IFuelTypeRepository
{
    public Task<FuelTypeListRepositoryDto> GetAllAsync(FuelTypeQueryRepositoryDto queryDto);

    public Task<FuelTypeRepositoryDto> GetByIdAsync(short id);

    public Task<FuelTypeRepositoryDto> AddAsync(AddFuelTypeRepositoryDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateFuelTypeRepositoryDto updateDto);
}