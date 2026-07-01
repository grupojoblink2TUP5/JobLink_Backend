using Application.DTOs.Experience.Request;
using Application.DTOs.Experience.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ExperienceService : IExperienceService
{
    private readonly IExperienceRepository _repository;

    public ExperienceService(IExperienceRepository repository)
    {
        _repository = repository;
    }

    public List<ExperienceResponse> GetAllExperiences()
    {
        return _repository
            .GetAll()
            .Select(experience => new ExperienceResponse(
                experience.Id,
                experience.CompanyName,
                experience.Position,
                experience.StartDate,
                experience.EndDate,
                experience.Description,
                experience.UserId
            ))
            .ToList();
    }

    public ExperienceResponse? GetExperienceById(int id)
    {
        var experience = _repository.GetById(id);

        if (experience == null)
        {
            return null;
        }

        return new ExperienceResponse(
            experience.Id,
            experience.CompanyName,
            experience.Position,
            experience.StartDate,
            experience.EndDate,
            experience.Description,
            experience.UserId
        );
    }

    public List<ExperienceResponse> GetExperiencesByUserId(int userId)
    {
        return _repository
            .GetByUserId(userId)
            .Select(experience => new ExperienceResponse(
                experience.Id,
                experience.CompanyName,
                experience.Position,
                experience.StartDate,
                experience.EndDate,
                experience.Description,
                experience.UserId
            ))
            .ToList();
    }

    public ExperienceResponse CreateExperience(CreateExperienceRequest request)
    {
        var experience = new Experience(
            request.CompanyName,
            request.Position,
            request.StartDate,
            request.EndDate,
            request.Description,
            request.UserId
        );

        _repository.Create(experience);
        _repository.SaveChanges();

        return new ExperienceResponse(
            experience.Id,
            experience.CompanyName,
            experience.Position,
            experience.StartDate,
            experience.EndDate,
            experience.Description,
            experience.UserId
        );
    }

    public ExperienceResponse UpdateExperience(int id, UpdateExperienceRequest request)
    {
        var experience = _repository.GetById(id);

        if (experience == null)
        {
            throw new NotFoundException($"Experience", id);
        }

        experience.UpdateExperience(
            request.CompanyName,
            request.Position,
            request.StartDate,
            request.EndDate,
            request.Description
        );

        _repository.Update(experience);
        _repository.SaveChanges();

        return new ExperienceResponse(
            experience.Id,
            experience.CompanyName,
            experience.Position,
            experience.StartDate,
            experience.EndDate,
            experience.Description,
            experience.UserId
        );
    }

    public bool DeleteExperience(int id)
    {
        var experience = _repository.GetById(id);

        if (experience == null)
        {
            return false;
        }

        _repository.Delete(experience);
        _repository.SaveChanges();

        return true;
    }
}