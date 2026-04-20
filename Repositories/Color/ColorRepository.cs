
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class ColorRepository(AppDbContext db) : IColorRepository
{ 
    DbSet<Color> set = db.Set<Color>();
    public async Task<ColorRepositoryDto> AddAsync(AddColorRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddColorRepositoryDto, Color>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddColorRepositoryDto, Color>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Color,ColorRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Color,ColorRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Color>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<ColorListRepositoryDto> GetAllAsync(ColorQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Color,ColorRepositoryDto>());
        var mapper = new Mapper(config);
        return new ColorListRepositoryDto()
        {
            Items = mapper.Map<List<ColorRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<ColorRepositoryDto> GetByIdAsync(int id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Color,ColorRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Color>(new {id});
        return mapper.Map<Color,ColorRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateColorRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Color>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }
		if(!String.IsNullOrEmpty(updateDto.HexCode)){
            entity.HexCode = updateDto.HexCode;
        }

        await db.SaveChangesAsync();
    }
}