
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class MaintenanceTypeRepository(AppDbContext db) : IMaintenanceTypeRepository
{ 
    DbSet<MaintenanceType> set = db.Set<MaintenanceType>();
    public async Task<MaintenanceTypeRepositoryDto> AddAsync(AddMaintenanceTypeRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddMaintenanceTypeRepositoryDto, MaintenanceType>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddMaintenanceTypeRepositoryDto, MaintenanceType>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceType,MaintenanceTypeRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<MaintenanceType,MaintenanceTypeRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<MaintenanceType>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<MaintenanceTypeListRepositoryDto> GetAllAsync(MaintenanceTypeQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceType,MaintenanceTypeRepositoryDto>());
        var mapper = new Mapper(config);
        return new MaintenanceTypeListRepositoryDto()
        {
            Items = mapper.Map<List<MaintenanceTypeRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<MaintenanceTypeRepositoryDto> GetByIdAsync(int id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<MaintenanceType,MaintenanceTypeRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<MaintenanceType>(new {id});
        return mapper.Map<MaintenanceType,MaintenanceTypeRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateMaintenanceTypeRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<MaintenanceType>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }

        await db.SaveChangesAsync();
    }
}