
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class VehicleModelRepository(AppDbContext db) : IVehicleModelRepository
{ 
    DbSet<VehicleModel> set = db.Set<VehicleModel>();
    public async Task<VehicleModelRepositoryDto> AddAsync(AddVehicleModelRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddVehicleModelRepositoryDto, VehicleModel>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddVehicleModelRepositoryDto, VehicleModel>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VehicleModel,VehicleModelRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<VehicleModel,VehicleModelRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<VehicleModel>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<VehicleModelListRepositoryDto> GetAllAsync(VehicleModelQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleModel,VehicleModelRepositoryDto>());
        var mapper = new Mapper(config);
        return new VehicleModelListRepositoryDto()
        {
            Items = mapper.Map<List<VehicleModelRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<VehicleModelRepositoryDto> GetByIdAsync(int id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleModel,VehicleModelRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<VehicleModel>(new {id});
        return mapper.Map<VehicleModel,VehicleModelRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateVehicleModelRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<VehicleModel>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }
		if(updateDto.ManufacturerId.HasValue){
            entity.ManufacturerId = updateDto.ManufacturerId.Value;
        }

		if(updateDto.VehicleCategoryId.HasValue){
            entity.VehicleCategoryId = updateDto.VehicleCategoryId.Value;
        }

		if(updateDto.Power.HasValue){
            entity.Power = updateDto.Power.Value;
        }
		if(updateDto.FuelTypeId.HasValue){
            entity.FuelTypeId = updateDto.FuelTypeId.Value;
        }

		if(updateDto.LoadCapacity.HasValue){
            entity.LoadCapacity = updateDto.LoadCapacity.Value;
        }


        await db.SaveChangesAsync();
    }
}