
namespace Global;
public interface IOwnershipHistoryRepository
{
    public Task<OwnershipHistoryListRepositoryDto> GetAllAsync(OwnershipHistoryQueryRepositoryDto queryDto);

    public Task<OwnershipHistoryRepositoryDto> GetByIdAsync(long id);

    public Task<OwnershipHistoryRepositoryDto> AddAsync(AddOwnershipHistoryRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateOwnershipHistoryRepositoryDto updateDto);
}