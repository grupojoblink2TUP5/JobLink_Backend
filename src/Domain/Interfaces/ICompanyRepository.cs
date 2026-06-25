using Domain.Entities;

namespace Domain.Interfaces;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(int id);

    Task<List<Company>> GetAllAsync();

    Task AddAsync(Company company);

    Task UpdateAsync(Company company);

    Task DeleteAsync(Company company);
}