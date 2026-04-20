
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class VehicleCategoryRepository(AppDbContext db) : IVehicleCategoryRepository
{ 
    DbSet<VehicleCategory> set = db.Set<VehicleCategory>();
    public async Task<VehicleCategoryRepositoryDto> AddAsync(AddVehicleCategoryRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddVehicleCategoryRepositoryDto, VehicleCategory>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddVehicleCategoryRepositoryDto, VehicleCategory>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VehicleCategory,VehicleCategoryRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<VehicleCategory,VehicleCategoryRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<VehicleCategory>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<VehicleCategoryListRepositoryDto> GetAllAsync(VehicleCategoryQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleCategory,VehicleCategoryRepositoryDto>());
        var mapper = new Mapper(config);
        return new VehicleCategoryListRepositoryDto()
        {
            Items = mapper.Map<List<VehicleCategoryRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<VehicleCategoryRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleCategory,VehicleCategoryRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<VehicleCategory>(new {id});
        return mapper.Map<VehicleCategory,VehicleCategoryRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateVehicleCategoryRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<VehicleCategory>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }

        await db.SaveChangesAsync();
    }
}