
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class FuelTypeRepository(AppDbContext db) : IFuelTypeRepository
{ 
    DbSet<FuelType> set = db.Set<FuelType>();
    public async Task<FuelTypeRepositoryDto> AddAsync(AddFuelTypeRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddFuelTypeRepositoryDto, FuelType>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddFuelTypeRepositoryDto, FuelType>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FuelType,FuelTypeRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<FuelType,FuelTypeRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<FuelType>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<FuelTypeListRepositoryDto> GetAllAsync(FuelTypeQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FuelType,FuelTypeRepositoryDto>());
        var mapper = new Mapper(config);
        return new FuelTypeListRepositoryDto()
        {
            Items = mapper.Map<List<FuelTypeRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<FuelTypeRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FuelType,FuelTypeRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<FuelType>(new {id});
        return mapper.Map<FuelType,FuelTypeRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateFuelTypeRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<FuelType>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }

        await db.SaveChangesAsync();
    }
}