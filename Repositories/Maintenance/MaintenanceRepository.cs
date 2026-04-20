
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class MaintenanceRepository(AppDbContext db) : IMaintenanceRepository
{ 
    DbSet<Maintenance> set = db.Set<Maintenance>();
    public async Task<MaintenanceRepositoryDto> AddAsync(AddMaintenanceRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddMaintenanceRepositoryDto, Maintenance>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddMaintenanceRepositoryDto, Maintenance>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Maintenance,MaintenanceRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Maintenance,MaintenanceRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Maintenance>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<MaintenanceListRepositoryDto> GetAllAsync(MaintenanceQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Maintenance,MaintenanceRepositoryDto>());
        var mapper = new Mapper(config);
        return new MaintenanceListRepositoryDto()
        {
            Items = mapper.Map<List<MaintenanceRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<MaintenanceRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Maintenance,MaintenanceRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Maintenance>(new {id});
        return mapper.Map<Maintenance,MaintenanceRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateMaintenanceRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Maintenance>(new {Id = updateDto.Id});
		if(updateDto.VehicleId.HasValue){
            entity.VehicleId = updateDto.VehicleId.Value;
        }

		if(updateDto.MaintenanceTypeId.HasValue){
            entity.MaintenanceTypeId = updateDto.MaintenanceTypeId.Value;
        }

		if(updateDto.ServiceDate.HasValue){
            entity.ServiceDate = updateDto.ServiceDate.Value;
        }

		if(!String.IsNullOrEmpty(updateDto.Description)){
            entity.Description = updateDto.Description;
        }
        await db.SaveChangesAsync();
    }
}