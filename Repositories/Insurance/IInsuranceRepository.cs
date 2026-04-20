
namespace Global;
public interface IInsuranceRepository
{
    public Task<InsuranceListRepositoryDto> GetAllAsync(InsuranceQueryRepositoryDto queryDto);

    public Task<InsuranceRepositoryDto> GetByIdAsync(long id);

    public Task<InsuranceRepositoryDto> AddAsync(AddInsuranceRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateInsuranceRepositoryDto updateDto);
}