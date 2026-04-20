
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class OwnershipHistoryService(IOwnershipHistoryRepository repository,
IVehicleRepository vehicleRepository,
IUserRepository userRepository,
ILogger<OwnershipHistoryService> logger) : IOwnershipHistoryService
{
    public async Task<OwnershipHistoryServiceDto> AddAsync(AddOwnershipHistoryServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddOwnershipHistoryServiceDto, AddOwnershipHistoryRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddOwnershipHistoryServiceDto, AddOwnershipHistoryRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        vehicleRepository.GetByIdAsync(addRepositoryDto.VehicleId),
		userRepository.GetByIdAsync(addRepositoryDto.UserId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<OwnershipHistoryRepositoryDto, OwnershipHistoryServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<OwnershipHistoryRepositoryDto, OwnershipHistoryServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(long id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<OwnershipHistoryListServiceDto> GetAllAsync(OwnershipHistoryQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<OwnershipHistoryQueryServiceDto,OwnershipHistoryQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<OwnershipHistoryQueryServiceDto,OwnershipHistoryQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<OwnershipHistoryRepositoryDto,OwnershipHistoryServiceDto>());
        var mapper2 = new Mapper(config2);
        return new OwnershipHistoryListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<OwnershipHistoryServiceDto>(x))
        };
    }

    public async Task<OwnershipHistoryServiceDto> GetByIdAsync(long id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<OwnershipHistoryRepositoryDto, OwnershipHistoryServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<OwnershipHistoryRepositoryDto, OwnershipHistoryServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateOwnershipHistoryServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateOwnershipHistoryServiceDto, UpdateOwnershipHistoryRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateOwnershipHistoryServiceDto, UpdateOwnershipHistoryRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.VehicleId.HasValue ? vehicleRepository.GetByIdAsync(updateDto.VehicleId.Value) : Task.CompletedTask,
		updateDto.UserId.HasValue ? userRepository.GetByIdAsync(updateDto.UserId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}