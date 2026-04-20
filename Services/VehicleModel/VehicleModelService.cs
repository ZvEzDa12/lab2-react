
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class VehicleModelService(IVehicleModelRepository repository,
IManufacturerRepository manufacturerRepository,
IVehicleCategoryRepository vehicleCategoryRepository,
IFuelTypeRepository fuelTypeRepository,
ILogger<VehicleModelService> logger) : IVehicleModelService
{
    public async Task<VehicleModelServiceDto> AddAsync(AddVehicleModelServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddVehicleModelServiceDto, AddVehicleModelRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddVehicleModelServiceDto, AddVehicleModelRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        manufacturerRepository.GetByIdAsync(addRepositoryDto.ManufacturerId),
		vehicleCategoryRepository.GetByIdAsync(addRepositoryDto.VehicleCategoryId),
		fuelTypeRepository.GetByIdAsync(addRepositoryDto.FuelTypeId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VehicleModelRepositoryDto, VehicleModelServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<VehicleModelRepositoryDto, VehicleModelServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(int id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<VehicleModelListServiceDto> GetAllAsync(VehicleModelQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleModelQueryServiceDto,VehicleModelQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<VehicleModelQueryServiceDto,VehicleModelQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VehicleModelRepositoryDto,VehicleModelServiceDto>());
        var mapper2 = new Mapper(config2);
        return new VehicleModelListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<VehicleModelServiceDto>(x))
        };
    }

    public async Task<VehicleModelServiceDto> GetByIdAsync(int id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleModelRepositoryDto, VehicleModelServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<VehicleModelRepositoryDto, VehicleModelServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateVehicleModelServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateVehicleModelServiceDto, UpdateVehicleModelRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateVehicleModelServiceDto, UpdateVehicleModelRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.ManufacturerId.HasValue ? manufacturerRepository.GetByIdAsync(updateDto.ManufacturerId.Value) : Task.CompletedTask,
		updateDto.VehicleCategoryId.HasValue ? vehicleCategoryRepository.GetByIdAsync(updateDto.VehicleCategoryId.Value) : Task.CompletedTask,
		updateDto.FuelTypeId.HasValue ? fuelTypeRepository.GetByIdAsync(updateDto.FuelTypeId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}