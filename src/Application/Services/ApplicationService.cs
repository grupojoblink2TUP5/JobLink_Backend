using Application.DTOs.Application.Request;
using Application.DTOs.Application.Response;
using Application.DTOs.ApplicationHistory.Request;
using ApplicationEntity = Domain.Entities.Application;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationHistoryService _applicationHistoryService;

    public ApplicationService(
        IApplicationRepository repository,
        IApplicationHistoryService applicationHistoryService)
    {
        _repository = repository;
        _applicationHistoryService = applicationHistoryService;
    }

    public List<ApplicationResponse> GetAllApplications()
    {
        return _repository
            .GetAll()
            .Select(MapToResponse)
            .ToList();
    }

    public ApplicationResponse GetApplicationById(int id)
    {
        var application = _repository.GetById(id);

        if (application == null)
            throw new NotFoundException($"Application not found for id = {id}");

        return MapToResponse(application);
    }

    public List<ApplicationResponse> GetApplicationsByUserId(int userId)
    {
        return _repository
            .GetByUserId(userId)
            .Select(MapToResponse)
            .ToList();
    }

    public List<ApplicationResponse> GetApplicationsByOfferId(int offerId)
    {
        return _repository
            .GetByOfferId(offerId)
            .Select(MapToResponse)
            .ToList();
    }

    public ApplicationResponse? GetApplicationByUserIdAndOfferId(int userId, int offerId)
    {
        var application = _repository.GetByUserIdAndOfferId(userId, offerId);

        if (application == null)
            return null;

        return MapToResponse(application);
    }

    public ApplicationResponse CreateApplication(int userId, CreateApplicationRequest request)
    {
        var existingApplication = _repository.GetByUserIdAndOfferId(userId, request.OfferId);
        if (existingApplication != null)
            throw new DuplicateApplicationException("El usuario ya tiene una postulación para esta oferta.");

        var application = new ApplicationEntity(userId, request.OfferId);

        _repository.Create(application);
        _repository.SaveChanges();

        var historyRequest = new CreateApplicationHistoryRequest
        {
            ChangedByRecruiterId = userId,
            Status = ApplicationStatus.Applied,
            Description = "Postulación creada",
            VisibleToCandidate = true
        };

        _applicationHistoryService.CreateApplicationHistory(application.Id, historyRequest);

        return MapToResponse(application);
    }

    public ApplicationResponse UpdateApplication(int id, UpdateApplicationRequest request)
    {
        var application = _repository.GetById(id);

        if (application == null)
            throw new NotFoundException($"Application not found for id = {id}");

        application.UpdateStatus(request.CurrentStatus);

        _repository.Update(application);
        _repository.SaveChanges();

        return MapToResponse(application);
    }

    public bool DeleteApplication(int id)
    {
        var application = _repository.GetById(id);

        if (application == null)
            return false;

        _repository.Delete(application);
        _repository.SaveChanges();

        return true;
    }

    private static ApplicationResponse MapToResponse(ApplicationEntity application)
    {
        return new ApplicationResponse(
            application.Id,
            application.CurrentStatus.ToString(),
            application.AppliedAt,
            application.UserId,
            application.OfferId
        );
    }
}