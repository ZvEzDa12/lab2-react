
namespace Global;
public interface IOwnershipHistoryService
{
    public Task<OwnershipHistoryListServiceDto> GetAllAsync(OwnershipHistoryQueryServiceDto queryDto);

    public Task<OwnershipHistoryServiceDto> GetByIdAsync(long id);

    public Task<OwnershipHistoryServiceDto> AddAsync(AddOwnershipHistoryServiceDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateOwnershipHistoryServiceDto updateDto);
}