using Application.DTOs.ApplicationHistory.Request;
using Application.DTOs.ApplicationHistory.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ApplicationHistoryService : IApplicationHistoryService
{
    private readonly IApplicationHistoryRepository _repository;

    public ApplicationHistoryService(IApplicationHistoryRepository repository)
    {
        _repository = repository;
    }

    public List<ApplicationHistoryResponse> GetAllApplicationHistories()
        => _repository.GetAll().Select(MapToResponse).ToList();

    public ApplicationHistoryResponse? GetApplicationHistoryById(int id)
    {
        var entity = _repository.GetById(id);
        return entity is null ? null : MapToResponse(entity);
    }

    public List<ApplicationHistoryResponse> GetApplicationHistoriesByApplicationId(int applicationId)
        => _repository.GetByApplicationId(applicationId)
            .Select(MapToResponse)
            .ToList();

    public ApplicationHistoryResponse CreateApplicationHistory(
        int applicationId,
        CreateApplicationHistoryRequest request)
    {
        var entity = new ApplicationHistory(
            applicationId,
            request.ChangedByRecruiterId,
            request.Status,
            request.Description,
            request.VisibleToCandidate
        );

        _repository.Create(entity);
        _repository.SaveChanges();

        return MapToResponse(entity);
    }

    public ApplicationHistoryResponse UpdateApplicationHistory(
        int id,
        UpdateApplicationHistoryRequest request)
    {
        var entity = _repository.GetById(id)
            ?? throw new NotFoundException($"ApplicationHistory no encontrado con id = {id}");

        entity.Update(request.Status, request.Description, request.VisibleToCandidate);

        _repository.Update(entity);
        _repository.SaveChanges();

        return MapToResponse(entity);
    }

    public bool DeleteApplicationHistory(int id)
    {
        var entity = _repository.GetById(id);
        if (entity is null) return false;

        _repository.Delete(entity);
        _repository.SaveChanges();

        return true;
    }

    private static ApplicationHistoryResponse MapToResponse(ApplicationHistory h) =>
        new(
            h.Id,
            h.ApplicationId,
            h.ChangedByRecruiterId,
            h.Status,
            h.Description,
            h.ChangedAt,
            h.VisibleToCandidate
        );
}