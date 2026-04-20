
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class ColorService(IColorRepository repository,

ILogger<ColorService> logger) : IColorService
{
    public async Task<ColorServiceDto> AddAsync(AddColorServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddColorServiceDto, AddColorRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddColorServiceDto, AddColorRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<ColorRepositoryDto, ColorServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<ColorRepositoryDto, ColorServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(int id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<ColorListServiceDto> GetAllAsync(ColorQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ColorQueryServiceDto,ColorQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<ColorQueryServiceDto,ColorQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<ColorRepositoryDto,ColorServiceDto>());
        var mapper2 = new Mapper(config2);
        return new ColorListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<ColorServiceDto>(x))
        };
    }

    public async Task<ColorServiceDto> GetByIdAsync(int id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ColorRepositoryDto, ColorServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<ColorRepositoryDto, ColorServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateColorServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateColorServiceDto, UpdateColorRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateColorServiceDto, UpdateColorRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}