using Application.DTOs.Application.Request;
using Application.DTOs.Application.Response;
using ApplicationEntity = Domain.Entities.Application;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;

    public ApplicationService(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public List<ApplicationResponse> GetAllApplications()
    {
        return _repository
            .GetAll()
            .Select(application => new ApplicationResponse(
                application.Id,
                application.CurrentStatus,
                application.AppliedAt,
                application.UserId,
                application.OfferId
            ))
            .ToList();
    }

    public ApplicationResponse? GetApplicationById(int id)
    {
        var application = _repository.GetById(id);

        if (application == null)
        {
            return null;
        }

        return new ApplicationResponse(
            application.Id,
            application.CurrentStatus,
            application.AppliedAt,
            application.UserId,
            application.OfferId
        );
    }

    public List<ApplicationResponse> GetApplicationsByUserId(int userId)
    {
        return _repository
            .GetByUserId(userId)
            .Select(application => new ApplicationResponse(
                application.Id,
                application.CurrentStatus,
                application.AppliedAt,
                application.UserId,
                application.OfferId
            ))
            .ToList();
    }

    public List<ApplicationResponse> GetApplicationsByOfferId(int offerId)
    {
        return _repository
            .GetByOfferId(offerId)
            .Select(application => new ApplicationResponse(
                application.Id,
                application.CurrentStatus,
                application.AppliedAt,
                application.UserId,
                application.OfferId
            ))
            .ToList();
    }

    public ApplicationResponse? GetApplicationByUserIdAndOfferId(int userId, int offerId)
    {
        var application = _repository.GetByUserIdAndOfferId(userId, offerId);

        if (application == null)
        {
            return null;
        }

        return new ApplicationResponse(
            application.Id,
            application.CurrentStatus,
            application.AppliedAt,
            application.UserId,
            application.OfferId
        );
    }

    public ApplicationResponse CreateApplication(int userId, CreateApplicationRequest request)
    {
        var application = new ApplicationEntity(
            userId,
            request.OfferId
        );

        _repository.Create(application);
        _repository.SaveChanges();

        return new ApplicationResponse(
            application.Id,
            application.CurrentStatus,
            application.AppliedAt,
            application.UserId,
            application.OfferId
        );
    }

    public ApplicationResponse UpdateApplication(int id, UpdateApplicationRequest request)
    {
        var application = _repository.GetById(id);

        if (application == null)
        {
            throw new NotFoundException($"Application not found for id = {id}");
        }

        if (!string.IsNullOrWhiteSpace(request.CurrentStatus))
        {
            application.UpdateStatus(request.CurrentStatus);
        }

        _repository.Update(application);
        _repository.SaveChanges();

        return new ApplicationResponse(
            application.Id,
            application.CurrentStatus,
            application.AppliedAt,
            application.UserId,
            application.OfferId
        );
    }

    public bool DeleteApplication(int id)
    {
        var application = _repository.GetById(id);

        if (application == null)
        {
            return false;
        }

        _repository.Delete(application);
        _repository.SaveChanges();

        return true;
    }
}