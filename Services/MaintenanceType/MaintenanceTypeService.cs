
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class MaintenanceTypeService(IMaintenanceTypeRepository repository,

ILogger<MaintenanceTypeService> logger) : IMaintenanceTypeService
{
    public async Task<MaintenanceTypeServiceDto> AddAsync(AddMaintenanceTypeServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddMaintenanceTypeServiceDto, AddMaintenanceTypeRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddMaintenanceTypeServiceDto, AddMaintenanceTypeRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceTypeRepositoryDto, MaintenanceTypeServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<MaintenanceTypeRepositoryDto, MaintenanceTypeServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(int id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<MaintenanceTypeListServiceDto> GetAllAsync(MaintenanceTypeQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceTypeQueryServiceDto,MaintenanceTypeQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<MaintenanceTypeQueryServiceDto,MaintenanceTypeQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceTypeRepositoryDto,MaintenanceTypeServiceDto>());
        var mapper2 = new Mapper(config2);
        return new MaintenanceTypeListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<MaintenanceTypeServiceDto>(x))
        };
    }

    public async Task<MaintenanceTypeServiceDto> GetByIdAsync(int id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceTypeRepositoryDto, MaintenanceTypeServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<MaintenanceTypeRepositoryDto, MaintenanceTypeServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateMaintenanceTypeServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateMaintenanceTypeServiceDto, UpdateMaintenanceTypeRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateMaintenanceTypeServiceDto, UpdateMaintenanceTypeRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}