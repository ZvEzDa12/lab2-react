
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class InsuranceRepository(AppDbContext db) : IInsuranceRepository
{ 
    DbSet<Insurance> set = db.Set<Insurance>();
    public async Task<InsuranceRepositoryDto> AddAsync(AddInsuranceRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddInsuranceRepositoryDto, Insurance>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddInsuranceRepositoryDto, Insurance>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Insurance,InsuranceRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Insurance,InsuranceRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Insurance>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<InsuranceListRepositoryDto> GetAllAsync(InsuranceQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Insurance,InsuranceRepositoryDto>());
        var mapper = new Mapper(config);
        return new InsuranceListRepositoryDto()
        {
            Items = mapper.Map<List<InsuranceRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<InsuranceRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Insurance,InsuranceRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Insurance>(new {id});
        return mapper.Map<Insurance,InsuranceRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateInsuranceRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Insurance>(new {Id = updateDto.Id});
		if(updateDto.VehicleId.HasValue){
            entity.VehicleId = updateDto.VehicleId.Value;
        }

		if(!String.IsNullOrEmpty(updateDto.PolicyNumber)){
            entity.PolicyNumber = updateDto.PolicyNumber;
        }
		if(!String.IsNullOrEmpty(updateDto.Company)){
            entity.Company = updateDto.Company;
        }
		if(updateDto.StartDate.HasValue){
            entity.StartDate = updateDto.StartDate.Value;
        }
		if(updateDto.EndDate.HasValue){
            entity.EndDate = updateDto.EndDate.Value;
        }
		if(updateDto.Cost.HasValue){
            entity.Cost = updateDto.Cost.Value;
        }
        await db.SaveChangesAsync();
    }
}