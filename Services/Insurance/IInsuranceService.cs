
namespace Global;
public interface IInsuranceService
{
    public Task<InsuranceListServiceDto> GetAllAsync(InsuranceQueryServiceDto queryDto);

    public Task<InsuranceServiceDto> GetByIdAsync(long id);

    public Task<InsuranceServiceDto> AddAsync(AddInsuranceServiceDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateInsuranceServiceDto updateDto);
}