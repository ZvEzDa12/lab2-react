
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class FuelTypeService(IFuelTypeRepository repository,

ILogger<FuelTypeService> logger) : IFuelTypeService
{
    public async Task<FuelTypeServiceDto> AddAsync(AddFuelTypeServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddFuelTypeServiceDto, AddFuelTypeRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddFuelTypeServiceDto, AddFuelTypeRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FuelTypeRepositoryDto, FuelTypeServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<FuelTypeRepositoryDto, FuelTypeServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<FuelTypeListServiceDto> GetAllAsync(FuelTypeQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FuelTypeQueryServiceDto,FuelTypeQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<FuelTypeQueryServiceDto,FuelTypeQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FuelTypeRepositoryDto,FuelTypeServiceDto>());
        var mapper2 = new Mapper(config2);
        return new FuelTypeListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<FuelTypeServiceDto>(x))
        };
    }

    public async Task<FuelTypeServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FuelTypeRepositoryDto, FuelTypeServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<FuelTypeRepositoryDto, FuelTypeServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateFuelTypeServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateFuelTypeServiceDto, UpdateFuelTypeRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateFuelTypeServiceDto, UpdateFuelTypeRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}