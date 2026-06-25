using Application.DTOs.Education.Request;
using Application.DTOs.Education.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class EducationService : IEducationService
{
    private readonly IEducationRepository _repository;

    public EducationService(IEducationRepository repository)
    {
        _repository = repository;
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

    public EducationResponse CreateEducation(CreateEducationRequest request)
    {
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

    public EducationResponse UpdateEducation(int id, UpdateEducationRequest request)
    {
        var education = _repository.GetById(id);

        if (education == null)
        {
            throw new NotFoundException($"Education not found for id = {id}");
        }

        education.UpdateEducation(
            request.InstitutionName,
            request.Degree,
            request.StartDate,
            request.EndDate
        );

        _repository.Update(education);
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

    public bool DeleteEducation(int id)
    {
        var education = _repository.GetById(id);

        if (education == null)
        {
            return false;
        }

        _repository.Delete(education);
        _repository.SaveChanges();

        return true;
    }
}