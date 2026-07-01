using Application.DTOs.ApplicationHistory.Request;
using Application.DTOs.ApplicationHistory.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ApplicationHistoryService : IApplicationHistoryService
{
    private readonly IApplicationHistoryRepository _repository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUserRepository _userRepository;

    public ApplicationHistoryService(
        IApplicationHistoryRepository repository,
        IApplicationRepository applicationRepository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _applicationRepository = applicationRepository;
        _userRepository = userRepository;
    }

    public async Task<List<ApplicationHistoryResponse>> GetAllApplicationHistoriesAsync()
    {
        var histories = await _repository.GetAllAsync();

        return histories
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<ApplicationHistoryResponse> GetApplicationHistoryByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            throw new NotFoundException("ApplicationHistory", id);

        return MapToResponse(entity);
    }

    public async Task<List<ApplicationHistoryResponse>> GetApplicationHistoriesByApplicationIdAsync(int applicationId)
    {
        var histories = await _repository.GetByApplicationIdAsync(applicationId);

        return histories
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<ApplicationHistoryResponse> CreateApplicationHistoryAsync(
        int applicationId,
        CreateApplicationHistoryRequest request)
    {
        if (request.ChangedByRecruiterId <= 0)
            throw new ArgumentException("ChangedByRecruiterId must be greater than zero.");

        if (!Enum.IsDefined(typeof(ApplicationStatus), request.Status))
            throw new InvalidStatusException();

        var recruiter = await _userRepository.GetByIdAsync(request.ChangedByRecruiterId);

        if (recruiter is null)
            throw new NotFoundException("User", request.ChangedByRecruiterId);

        var application = await _applicationRepository.GetByIdAsync(applicationId);

        if (application is null)
            throw new NotFoundException("Application", applicationId);

        var entity = new ApplicationHistory(
            applicationId,
            request.ChangedByRecruiterId,
            request.Status,
            request.Description,
            request.VisibleToCandidate
        );

        _repository.Create(entity);
        await _repository.SaveChangesAsync();

        application.UpdateStatus(request.Status);
        _applicationRepository.Update(application);
        await _applicationRepository.SaveChangesAsync();

        return MapToResponse(entity);
    }

    public async Task<ApplicationHistoryResponse> UpdateApplicationHistoryAsync(
        int id,
        UpdateApplicationHistoryRequest request)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            throw new NotFoundException("ApplicationHistory", id);

        var latest = await GetLatestAsync(entity.ApplicationId);

        if (latest is not null && latest.Id != entity.Id)
            throw new ForbiddenException();

        entity.UpdateDetails(request.Description, request.VisibleToCandidate);

        _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return MapToResponse(entity);
    }

    public async Task<bool> DeleteApplicationHistoryAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            throw new NotFoundException("ApplicationHistory", id);

        var allForApplication = await _repository.GetByApplicationIdAsync(entity.ApplicationId);

        if (allForApplication.Count <= 1)
            throw new ForbiddenException();

        _repository.Delete(entity);
        await _repository.SaveChangesAsync();

        return true;
    }

    private async Task<ApplicationHistory?> GetLatestAsync(int applicationId)
    {
        var histories = await _repository.GetByApplicationIdAsync(applicationId);

        return histories
            .OrderByDescending(h => h.ChangedAt)
            .FirstOrDefault();
    }

    private static ApplicationHistoryResponse MapToResponse(ApplicationHistory h)
    {
        return new ApplicationHistoryResponse(
            h.Id,
            h.ApplicationId,
            h.ChangedByRecruiterId,
            h.Status,
            h.Description,
            h.ChangedAt,
            h.VisibleToCandidate
        );
    }
}