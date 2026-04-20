
namespace Global;
public interface IColorService
{
    public Task<ColorListServiceDto> GetAllAsync(ColorQueryServiceDto queryDto);

    public Task<ColorServiceDto> GetByIdAsync(int id);

    public Task<ColorServiceDto> AddAsync(AddColorServiceDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateColorServiceDto updateDto);
}