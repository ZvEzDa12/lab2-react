
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class VehicleRepository(AppDbContext db) : IVehicleRepository
{ 
    DbSet<Vehicle> set = db.Set<Vehicle>();
    public async Task<VehicleRepositoryDto> AddAsync(AddVehicleRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddVehicleRepositoryDto, Vehicle>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddVehicleRepositoryDto, Vehicle>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle,VehicleRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Vehicle,VehicleRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Vehicle>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<VehicleListRepositoryDto> GetAllAsync(VehicleQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle,VehicleRepositoryDto>());
        var mapper = new Mapper(config);
        return new VehicleListRepositoryDto()
        {
            Items = mapper.Map<List<VehicleRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<VehicleRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle,VehicleRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Vehicle>(new {id});
        return mapper.Map<Vehicle,VehicleRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateVehicleRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Vehicle>(new {Id = updateDto.Id});
		if(updateDto.ModelId.HasValue){
            entity.ModelId = updateDto.ModelId.Value;
        }



		if(!String.IsNullOrEmpty(updateDto.Vin)){
            entity.Vin = updateDto.Vin;
        }
		if(updateDto.ProductionYear.HasValue){
            entity.ProductionYear = updateDto.ProductionYear.Value;
        }
        if(updateDto.Mileage.HasValue){
            entity.Mileage = updateDto.Mileage.Value;
        }
		var regNumberEntry = db.Entry(entity).Property(x => x.RegistrationNumber);
		var newRegNumber = string.IsNullOrEmpty(updateDto.RegistrationNumber) ? null : updateDto.RegistrationNumber;
		if(regNumberEntry.CurrentValue != newRegNumber || (regNumberEntry.CurrentValue == null && newRegNumber == null)){
			regNumberEntry.CurrentValue = newRegNumber;
			regNumberEntry.IsModified = true;
		}
		if(updateDto.ColorId.HasValue){
            entity.ColorId = updateDto.ColorId.Value;
        } else {
            entity.ColorId = null;
        }



        await db.SaveChangesAsync();
    }
}