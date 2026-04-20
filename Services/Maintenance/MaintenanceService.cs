
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class MaintenanceService(IMaintenanceRepository repository,
IVehicleRepository vehicleRepository,
IMaintenanceTypeRepository maintenanceTypeRepository,
ILogger<MaintenanceService> logger) : IMaintenanceService
{
    public async Task<MaintenanceServiceDto> AddAsync(AddMaintenanceServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddMaintenanceServiceDto, AddMaintenanceRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddMaintenanceServiceDto, AddMaintenanceRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        vehicleRepository.GetByIdAsync(addRepositoryDto.VehicleId),
		maintenanceTypeRepository.GetByIdAsync(addRepositoryDto.MaintenanceTypeId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceRepositoryDto, MaintenanceServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<MaintenanceRepositoryDto, MaintenanceServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(long id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<MaintenanceListServiceDto> GetAllAsync(MaintenanceQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceQueryServiceDto,MaintenanceQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<MaintenanceQueryServiceDto,MaintenanceQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceRepositoryDto,MaintenanceServiceDto>());
        var mapper2 = new Mapper(config2);
        return new MaintenanceListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<MaintenanceServiceDto>(x))
        };
    }

    public async Task<MaintenanceServiceDto> GetByIdAsync(long id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceRepositoryDto, MaintenanceServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<MaintenanceRepositoryDto, MaintenanceServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateMaintenanceServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateMaintenanceServiceDto, UpdateMaintenanceRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateMaintenanceServiceDto, UpdateMaintenanceRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.VehicleId.HasValue ? vehicleRepository.GetByIdAsync(updateDto.VehicleId.Value) : Task.CompletedTask,
		updateDto.MaintenanceTypeId.HasValue ? maintenanceTypeRepository.GetByIdAsync(updateDto.MaintenanceTypeId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}