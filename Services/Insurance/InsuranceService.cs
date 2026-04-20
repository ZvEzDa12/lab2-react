
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class InsuranceService(IInsuranceRepository repository,
IVehicleRepository vehicleRepository,
ILogger<InsuranceService> logger) : IInsuranceService
{
    public async Task<InsuranceServiceDto> AddAsync(AddInsuranceServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddInsuranceServiceDto, AddInsuranceRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddInsuranceServiceDto, AddInsuranceRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        vehicleRepository.GetByIdAsync(addRepositoryDto.VehicleId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<InsuranceRepositoryDto, InsuranceServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<InsuranceRepositoryDto, InsuranceServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(long id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<InsuranceListServiceDto> GetAllAsync(InsuranceQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<InsuranceQueryServiceDto,InsuranceQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<InsuranceQueryServiceDto,InsuranceQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<InsuranceRepositoryDto,InsuranceServiceDto>());
        var mapper2 = new Mapper(config2);
        return new InsuranceListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<InsuranceServiceDto>(x))
        };
    }

    public async Task<InsuranceServiceDto> GetByIdAsync(long id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<InsuranceRepositoryDto, InsuranceServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<InsuranceRepositoryDto, InsuranceServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateInsuranceServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateInsuranceServiceDto, UpdateInsuranceRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateInsuranceServiceDto, UpdateInsuranceRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.VehicleId.HasValue ? vehicleRepository.GetByIdAsync(updateDto.VehicleId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}