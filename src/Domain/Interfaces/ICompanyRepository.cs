using Domain.Entities;

namespace Domain.Interfaces;

public interface ICompanyRepository
{
    List<Company> GetAll();

    Company? GetById(int id);

    Company Create(Company company);

    void Update(Company company);

    void Delete(Company company);

    void SaveChanges();
}