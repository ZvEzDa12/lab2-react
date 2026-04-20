
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class ManufacturerRepository(AppDbContext db) : IManufacturerRepository
{ 
    DbSet<Manufacturer> set = db.Set<Manufacturer>();
    public async Task<ManufacturerRepositoryDto> AddAsync(AddManufacturerRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddManufacturerRepositoryDto, Manufacturer>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddManufacturerRepositoryDto, Manufacturer>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer,ManufacturerRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Manufacturer,ManufacturerRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Manufacturer>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<ManufacturerListRepositoryDto> GetAllAsync(ManufacturerQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer,ManufacturerRepositoryDto>());
        var mapper = new Mapper(config);
        return new ManufacturerListRepositoryDto()
        {
            Items = mapper.Map<List<ManufacturerRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<ManufacturerRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer,ManufacturerRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Manufacturer>(new {id});
        return mapper.Map<Manufacturer,ManufacturerRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateManufacturerRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Manufacturer>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }
		if(!String.IsNullOrEmpty(updateDto.Country)){
            entity.Country = updateDto.Country;
        }


        await db.SaveChangesAsync();
    }
}