using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    List<User> GetAll();

    User? GetById(int id);

    User? GetByEmail(string email);

    User Create(User user);

    void Update(User user);

    void Delete(User user);

    void SaveChanges();
}