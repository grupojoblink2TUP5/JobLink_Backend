using Domain.Entities;

namespace Domain.Interfaces;

public interface ICandidateRepository
{
    List<Candidate> GetAll();

    Candidate? GetById(int id);

    Candidate? GetByUserId(int userId);

    Candidate Create(Candidate candidate);

    void Update(Candidate candidate);

    void Delete(Candidate candidate);

    void SaveChanges();
}