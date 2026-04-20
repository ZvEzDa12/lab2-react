
namespace Global;
public interface IColorRepository
{
    public Task<ColorListRepositoryDto> GetAllAsync(ColorQueryRepositoryDto queryDto);

    public Task<ColorRepositoryDto> GetByIdAsync(int id);

    public Task<ColorRepositoryDto> AddAsync(AddColorRepositoryDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateColorRepositoryDto updateDto);
}