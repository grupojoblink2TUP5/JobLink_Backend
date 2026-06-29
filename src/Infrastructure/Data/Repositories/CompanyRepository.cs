using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationContext _context;

    public CompanyRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies
            .FirstOrDefaultAsync(
                c => c.Id == id);
    }

    public async Task<List<Company>> GetAllAsync()
    {
        return await _context.Companies
            .ToListAsync();
    }

    public async Task AddAsync(
        Company company)
    {
        await _context.Companies.AddAsync(
            company);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(
        Company company)
    {
        _context.Companies.Update(
            company);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        Company company)
    {
        _context.Companies.Remove(
            company);

        await _context.SaveChangesAsync();
    }
}