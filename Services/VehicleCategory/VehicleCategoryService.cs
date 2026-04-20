
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class VehicleCategoryService(IVehicleCategoryRepository repository,

ILogger<VehicleCategoryService> logger) : IVehicleCategoryService
{
    public async Task<VehicleCategoryServiceDto> AddAsync(AddVehicleCategoryServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddVehicleCategoryServiceDto, AddVehicleCategoryRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddVehicleCategoryServiceDto, AddVehicleCategoryRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VehicleCategoryRepositoryDto, VehicleCategoryServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<VehicleCategoryRepositoryDto, VehicleCategoryServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<VehicleCategoryListServiceDto> GetAllAsync(VehicleCategoryQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleCategoryQueryServiceDto,VehicleCategoryQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<VehicleCategoryQueryServiceDto,VehicleCategoryQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VehicleCategoryRepositoryDto,VehicleCategoryServiceDto>());
        var mapper2 = new Mapper(config2);
        return new VehicleCategoryListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<VehicleCategoryServiceDto>(x))
        };
    }

    public async Task<VehicleCategoryServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleCategoryRepositoryDto, VehicleCategoryServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<VehicleCategoryRepositoryDto, VehicleCategoryServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateVehicleCategoryServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateVehicleCategoryServiceDto, UpdateVehicleCategoryRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateVehicleCategoryServiceDto, UpdateVehicleCategoryRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}