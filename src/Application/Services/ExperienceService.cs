using Application.DTOs.Experience.Request;
using Application.DTOs.Experience.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ExperienceService : IExperienceService
{
    private readonly IExperienceRepository _repository;
    private readonly IUserRepository _userRepository;

    public ExperienceService(
        IExperienceRepository repository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
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

    public async Task<ExperienceResponse> CreateExperienceAsync(
        CreateExperienceRequest request)
    {
        ValidateDates(request.StartDate, request.EndDate);

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new NotFoundException($"User not found for id = {request.UserId}");
        }

        if (user.Role != UserRole.Candidate)
        {
            throw new UserIsNotCandidateException(user.Email);
        }

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
        ValidateDates(request.StartDate, request.EndDate);

        var experience = _repository.GetById(id);

        if (experience == null)
        {
            throw new NotFoundException($"Experience not found for id = {id}");
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

    private static void ValidateDates(DateTime startDate, DateTime? endDate)
    {
        if (endDate.HasValue && endDate.Value <= startDate)
        {
            throw new InvalidExperienceDateException();
        }
    }
}