
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class ManufacturerService(IManufacturerRepository repository,

ILogger<ManufacturerService> logger) : IManufacturerService
{
    public async Task<ManufacturerServiceDto> AddAsync(AddManufacturerServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddManufacturerServiceDto, AddManufacturerRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddManufacturerServiceDto, AddManufacturerRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<ManufacturerRepositoryDto, ManufacturerServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<ManufacturerRepositoryDto, ManufacturerServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(long id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<ManufacturerListServiceDto> GetAllAsync(ManufacturerQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ManufacturerQueryServiceDto,ManufacturerQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<ManufacturerQueryServiceDto,ManufacturerQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<ManufacturerRepositoryDto,ManufacturerServiceDto>());
        var mapper2 = new Mapper(config2);
        return new ManufacturerListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<ManufacturerServiceDto>(x))
        };
    }

    public async Task<ManufacturerServiceDto> GetByIdAsync(long id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ManufacturerRepositoryDto, ManufacturerServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<ManufacturerRepositoryDto, ManufacturerServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateManufacturerServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateManufacturerServiceDto, UpdateManufacturerRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateManufacturerServiceDto, UpdateManufacturerRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}