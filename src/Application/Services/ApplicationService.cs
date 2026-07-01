using Application.DTOs.Application.Request;
using Application.DTOs.Application.Response;
using Application.DTOs.ApplicationHistory.Request;
using ApplicationEntity = Domain.Entities.Application;
using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationHistoryService _applicationHistoryService;
    private readonly IJobOfferRepository _jobOfferRepository;
    private readonly IUserRepository _userRepository;

    public ApplicationService(
        IApplicationRepository repository,
        IApplicationHistoryService applicationHistoryService,
        IJobOfferRepository jobOfferRepository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _applicationHistoryService = applicationHistoryService;
        _jobOfferRepository = jobOfferRepository;
        _userRepository = userRepository;
    }

    public async Task<List<ApplicationResponse>> GetAllApplicationsAsync()
    {
        var applications = await _repository.GetAllAsync();

        return applications
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<ApplicationResponse> GetApplicationByIdAsync(int id)
    {
        var application = await _repository.GetByIdAsync(id);

        if (application is null)
            throw new NotFoundException($"Application not found. Id = {id}");

        return MapToResponse(application);
    }

    public async Task<List<ApplicationResponse>> GetApplicationsByUserIdAsync(int userId)
    {
        var applications = await _repository.GetByUserIdAsync(userId);

        if (applications.Count == 0)
            throw new NotFoundException($"No applications found for user id = {userId}");

        return applications
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<List<ApplicationResponse>> GetApplicationsByOfferIdAsync(int offerId)
    {
        var applications = await _repository.GetByOfferIdAsync(offerId);

        return applications
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<ApplicationResponse?> GetApplicationByUserIdAndOfferIdAsync(int userId, int offerId)
    {
        var application = await _repository.GetByUserIdAndOfferIdAsync(userId, offerId);

        if (application is null)
            return null;

        return MapToResponse(application);
    }

    public async Task<ApplicationResponse> CreateApplicationAsync(int userId, CreateApplicationRequest request)
    {
        if (request.OfferId <= 0)
            throw new ArgumentOutOfRangeException(nameof(request.OfferId), "OfferId must be greater than zero.");

        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException($"User not found. Id = {userId}");

        if (!user.Status)
            throw new InvalidOperationException("The user is not active.");

        if (user.Role != UserRole.Candidate)
            throw new InvalidOperationException("Only candidates can apply to job offers.");

        var offer = await _jobOfferRepository.GetByIdAsync(request.OfferId);

        if (offer is null)
            throw new NotFoundException($"JobOffer not found. Id = {request.OfferId}");

        if (offer.Status != JobOfferStatus.Open)
            throw new InvalidOperationException("Cannot apply to a job offer that is not open.");

        if (offer.ClosingDate <= DateTime.UtcNow)
            throw new InvalidOperationException("Job offer has expired.");

        var existingApplication = await _repository.GetByUserIdAndOfferIdAsync(userId, request.OfferId);

        if (existingApplication is not null)
            throw new DuplicateApplicationException(userId, request.OfferId);

        var application = new ApplicationEntity(userId, request.OfferId);

        _repository.Create(application);
        await _repository.SaveChangesAsync();

        var historyRequest = new CreateApplicationHistoryRequest
        {
            ChangedByRecruiterId = userId,
            Status = ApplicationStatus.Applied,
            Description = "Application created",
            VisibleToCandidate = true
        };

        await _applicationHistoryService.CreateApplicationHistoryAsync(application.Id, historyRequest);

        return MapToResponse(application);
    }

    public async Task<bool> DeleteApplicationAsync(int id)
    {
        var application = await _repository.GetByIdAsync(id);

        if (application is null)
            throw new NotFoundException($"Application not found. Id = {id}");

        _repository.Delete(application);
        await _repository.SaveChangesAsync();

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