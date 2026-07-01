using Application.DTOs.Education.Request;
using Application.DTOs.Education.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class EducationService : IEducationService
{
    private readonly IEducationRepository _repository;
    private readonly IUserRepository _userRepository;

    public EducationService(
        IEducationRepository repository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public List<EducationResponse> GetAllEducations()
    {
        return _repository
            .GetAll()
            .Select(education => new EducationResponse(
                education.Id,
                education.InstitutionName,
                education.Degree,
                education.StartDate,
                education.EndDate,
                education.UserId
            ))
            .ToList();
    }

    public EducationResponse? GetEducationById(int id)
    {
        var education = _repository.GetById(id);

        if (education == null)
        {
            return null;
        }

        return new EducationResponse(
            education.Id,
            education.InstitutionName,
            education.Degree,
            education.StartDate,
            education.EndDate,
            education.UserId
        );
    }

    public List<EducationResponse> GetEducationsByUserId(int userId)
    {
        return _repository
            .GetByUserId(userId)
            .Select(education => new EducationResponse(
                education.Id,
                education.InstitutionName,
                education.Degree,
                education.StartDate,
                education.EndDate,
                education.UserId
            ))
            .ToList();
    }

    public async Task<EducationResponse> CreateEducationAsync(
        CreateEducationRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new NotFoundException("User", request.UserId);
        }

        if (user.Role != UserRole.Candidate)
        {
            throw new UserIsNotCandidateException(user.Email);
        }

        ValidateDates(request.StartDate, request.EndDate);

        var education = new Education(
            request.InstitutionName,
            request.Degree,
            request.StartDate,
            request.EndDate,
            request.UserId
        );

        _repository.Create(education);
        _repository.SaveChanges();

        return new EducationResponse(
            education.Id,
            education.InstitutionName,
            education.Degree,
            education.StartDate,
            education.EndDate,
            education.UserId
        );
    }

    public void UpdateEducation(int id, UpdateEducationRequest request)
    {
        var education = _repository.GetById(id);

        if (education == null)
        {
            throw new NotFoundException("Education", id);
        }

        ValidateDates(request.StartDate, request.EndDate);

        education.UpdateEducation(
            request.InstitutionName,
            request.Degree,
            request.StartDate,
            request.EndDate
        );

        _repository.Update(education);
        _repository.SaveChanges();
    }

    public void DeleteEducation(int id)
    {
        var education = _repository.GetById(id);

        if (education == null)
        {
            throw new NotFoundException("Education", id);
        }

        _repository.Delete(education);
        _repository.SaveChanges();
    }

    private static void ValidateDates(DateTime startDate, DateTime? endDate)
    {
        if (endDate.HasValue && endDate.Value <= startDate)
        {
            throw new InvalidEducationDateException();
        }
    }
}