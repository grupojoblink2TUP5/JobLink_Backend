using Application.DTOs.JobOffer.Request;
using Application.DTOs.JobOffer.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;
using Domain.Enums;

namespace Application.Services;

public class JobOfferService : IJobOfferService
{
    private readonly IJobOfferRepository _jobOfferRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;

    public JobOfferService(
    IJobOfferRepository jobOfferRepository,
    ICompanyRepository companyRepository,
    IUserRepository userRepository)
    {
        _jobOfferRepository = jobOfferRepository;
        _companyRepository = companyRepository;
        _userRepository = userRepository;
    }

    public async Task<JobOfferResponseDto> CreateAsync(
    CreateJobOfferRequestDto request)
    {
        var company =
    await _companyRepository
        .GetByIdAsync(request.CompanyId);

        if (company is null)
        {
            throw new NotFoundException(
                $"Company not found. Id = {request.CompanyId}");
        }

        if (!company.Status)
        {
            throw new InvalidOperationException(
                "Company has not been approved.");
        }

        var recruiter =
            await _userRepository
                .GetByIdAsync(
                    request.CreatedByRecruiterId);

        if (recruiter is null)
        {
            throw new NotFoundException(
                $"Recruiter not found. Id = {request.CreatedByRecruiterId}");
        }

        if (recruiter.Role != UserRole.Recruiter)
        {
            throw new InvalidOperationException(
                "The specified user is not a recruiter.");
        }

        if (company.CreatedByRecruiterId != recruiter.Id)
        {
            throw new InvalidOperationException(
                "The recruiter does not own this company.");
        }

        if (request.Salary <= 0)
            throw new ArgumentException(
                "Salary must be greater than zero.");

        if (request.ClosingDate <= DateTime.UtcNow)
            throw new ArgumentException(
                "Closing date must be in the future.");

        var jobOffer = new JobOffer(
            request.JobTitle,
            request.Description,
            request.Salary,
            request.Location,
            request.OfferType,
            request.ClosingDate,
            request.CompanyId,
            request.CreatedByRecruiterId);

        await _jobOfferRepository.AddAsync(jobOffer);

        return MapToResponse(jobOffer);
    }

    public async Task<List<JobOfferResponseDto>> GetAllAsync()
    {
        var offers =
            await _jobOfferRepository.GetAllAsync();

        return offers
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<JobOfferResponseDto?> GetByIdAsync(
        int id)
    {
        var offer =
            await _jobOfferRepository.GetByIdAsync(id);

        if (offer is null)
            return null;

        return MapToResponse(offer);
    }

    public async Task<List<JobOfferResponseDto>>
        GetByCompanyIdAsync(int companyId)
    {
        var offers =
            await _jobOfferRepository
                .GetByCompanyIdAsync(companyId);

        return offers
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<List<JobOfferResponseDto>>
        GetOpenOffersAsync()
    {
        var offers =
            await _jobOfferRepository
                .GetOpenOffersAsync();

        return offers
            .Select(MapToResponse)
            .ToList();
    }

    public async Task UpdateAsync(
        int id,
        UpdateJobOfferRequestDto request)
    {
        var offer =
            await _jobOfferRepository
                .GetByIdAsync(id);

        if (offer is null)
            throw new NotFoundException("Job offer not found.");

        offer.Update(
            request.JobTitle,
            request.Description,
            request.Salary,
            request.Location,
            request.OfferType,
            request.ClosingDate);

        await _jobOfferRepository.UpdateAsync(offer);
    }

    public async Task DeleteAsync(int id)
    {
        var offer =
            await _jobOfferRepository
                .GetByIdAsync(id);

        if (offer is null)
            throw new NotFoundException("Job offer not found.");

        await _jobOfferRepository.DeleteAsync(offer);
    }

    public async Task CloseAsync(int id)
    {
        var offer =
            await _jobOfferRepository
                .GetByIdAsync(id);

        if (offer is null)
            throw new NotFoundException("Job offer not found.");

        offer.Close();

        await _jobOfferRepository.UpdateAsync(offer);
    }

    public async Task PauseAsync(int id)
    {
        var offer =
            await _jobOfferRepository
                .GetByIdAsync(id);

        if (offer is null)
            throw new NotFoundException("Job offer not found.");

        offer.Pause();

        await _jobOfferRepository.UpdateAsync(offer);
    }

    public async Task ReopenAsync(int id)
    {
        var offer =
            await _jobOfferRepository
                .GetByIdAsync(id);

        if (offer is null)
            throw new NotFoundException("Job offer not found.");

        offer.Reopen();

        await _jobOfferRepository.UpdateAsync(offer);
    }

    private static JobOfferResponseDto MapToResponse(
        JobOffer offer)
    {
        return new JobOfferResponseDto
        {
            Id = offer.Id,
            JobTitle = offer.JobTitle,
            Description = offer.Description,
            Salary = offer.Salary,
            Location = offer.Location,
            OfferType = offer.OfferType,
            Status = offer.Status,
            PublicationDate = offer.PublicationDate,
            ClosingDate = offer.ClosingDate,
            CompanyId = offer.CompanyId,
            CreatedByRecruiterId =
                offer.CreatedByRecruiterId
        };
    }
}