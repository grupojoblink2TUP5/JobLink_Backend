using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CvRepository : ICvRepository
{
    private readonly ApplicationContext _context;

    public CvRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Cv?> GetByUserIdAsync(
        int userId)
    {
        return await _context.Cvs
            .FirstOrDefaultAsync(
                cv => cv.UserId == userId);
    }

    public async Task AddAsync(
        Cv cv)
    {
        await _context.Cvs.AddAsync(cv);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(
        Cv cv)
    {
        _context.Cvs.Update(cv);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        Cv cv)
    {
        _context.Cvs.Remove(cv);

        await _context.SaveChangesAsync();
    }
}