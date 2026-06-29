using Domain.Entities;

namespace Domain.Interfaces;

public interface ICvRepository
{
    Task<Cv?> GetByUserIdAsync(int userId);

    Task AddAsync(Cv cv);

    Task UpdateAsync(Cv cv);

    Task DeleteAsync(Cv cv);
}