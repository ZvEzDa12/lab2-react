
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class OwnershipHistoryRepository(AppDbContext db) : IOwnershipHistoryRepository
{ 
    DbSet<OwnershipHistory> set = db.Set<OwnershipHistory>();
    public async Task<OwnershipHistoryRepositoryDto> AddAsync(AddOwnershipHistoryRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddOwnershipHistoryRepositoryDto, OwnershipHistory>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddOwnershipHistoryRepositoryDto, OwnershipHistory>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<OwnershipHistory,OwnershipHistoryRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<OwnershipHistory,OwnershipHistoryRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<OwnershipHistory>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<OwnershipHistoryListRepositoryDto> GetAllAsync(OwnershipHistoryQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<OwnershipHistory,OwnershipHistoryRepositoryDto>());
        var mapper = new Mapper(config);
        return new OwnershipHistoryListRepositoryDto()
        {
            Items = mapper.Map<List<OwnershipHistoryRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<OwnershipHistoryRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<OwnershipHistory,OwnershipHistoryRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<OwnershipHistory>(new {id});
        return mapper.Map<OwnershipHistory,OwnershipHistoryRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateOwnershipHistoryRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<OwnershipHistory>(new {Id = updateDto.Id});
		if(updateDto.VehicleId.HasValue){
            entity.VehicleId = updateDto.VehicleId.Value;
        }

		if(updateDto.UserId.HasValue){
            entity.UserId = updateDto.UserId.Value;
        }

		if(updateDto.StartDate.HasValue){
            entity.StartDate = updateDto.StartDate.Value;
        }

        await db.SaveChangesAsync();
    }
}