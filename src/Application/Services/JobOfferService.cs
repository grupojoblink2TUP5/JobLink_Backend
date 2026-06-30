using Application.DTOs.JobOffer.Request;
using Application.DTOs.JobOffer.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

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
        if (string.IsNullOrWhiteSpace(request.JobTitle))
        {
            throw new FieldRequiredException("JobTitle");
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new FieldRequiredException("Description");
        }

        if (string.IsNullOrWhiteSpace(request.Location))
        {
            throw new FieldRequiredException("Location");
        }

        var company =
            await GetCompanyOrThrowAsync(
                request.CompanyId);

        if (!company.Status)
            throw new CompanyNotApprovedException(
                company.BusinessName);

        var recruiter =
            await GetRecruiterOrThrowAsync(
                request.CreatedByRecruiterId);

        if (recruiter.Role != UserRole.Recruiter)
            throw new UserIsNotRecruiterException(
                recruiter.Email);

        if (company.CreatedByRecruiterId != recruiter.Id)
            throw new RecruiterDoesNotOwnCompanyException();

        if (request.Salary <= 0)
            throw new InvalidSalaryException(
                request.Salary);

        if (!Enum.IsDefined(typeof(OfferType), request.OfferType))
        {
            throw new InvalidOfferTypeException(request.OfferType);
        }

        if (request.ClosingDate <= DateTime.UtcNow)
            throw new InvalidClosingDateException(
                request.ClosingDate);

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

    public async Task<JobOfferResponseDto> GetByIdAsync(int id)
    {
        var offer =
            await GetJobOfferOrThrowAsync(id);

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
        if (!Enum.IsDefined(typeof(OfferType), request.OfferType))
        {
            throw new InvalidOfferTypeException(request.OfferType);
        }

        if (request.Salary <= 0)
        {
            throw new InvalidSalaryException(request.Salary);
        }

        if (request.ClosingDate <= DateTime.UtcNow)
        {
            throw new InvalidClosingDateException(request.ClosingDate);
        }

        if (string.IsNullOrWhiteSpace(request.JobTitle))
        {
            throw new FieldRequiredException("JobTitle");
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new FieldRequiredException("Description");
        }

        if (string.IsNullOrWhiteSpace(request.Location))
        {
            throw new FieldRequiredException("Location");
        }

        var offer =
            await GetJobOfferOrThrowAsync(id);


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
            await GetJobOfferOrThrowAsync(id);

        await _jobOfferRepository.DeleteAsync(offer);
    }

    public async Task CloseAsync(int id)
    {
        var offer =
            await GetJobOfferOrThrowAsync(id);

        offer.Close();

        await _jobOfferRepository.UpdateAsync(offer);
    }

    public async Task PauseAsync(int id)
    {
        var offer =
            await GetJobOfferOrThrowAsync(id);

        offer.Pause();

        await _jobOfferRepository.UpdateAsync(offer);
    }

    public async Task ReopenAsync(int id)
    {
        var offer =
            await GetJobOfferOrThrowAsync(id);

        offer.Reopen();

        await _jobOfferRepository.UpdateAsync(offer);
    }

    private async Task<JobOffer> GetJobOfferOrThrowAsync(
        int id)
    {
        var offer =
            await _jobOfferRepository
                .GetByIdAsync(id);

        if (offer is null)
            throw new NotFoundException(
                nameof(JobOffer),
                id);

        return offer;
    }

    private async Task<Company> GetCompanyOrThrowAsync(
        int id)
    {
        var company =
            await _companyRepository
                .GetByIdAsync(id);

        if (company is null)
            throw new NotFoundException(
                nameof(Company),
                id);

        return company;
    }

    private async Task<User> GetRecruiterOrThrowAsync(
        int id)
    {
        var recruiter =
            await _userRepository
                .GetByIdAsync(id);

        if (recruiter is null)
            throw new NotFoundException(
                "Recruiter",
                id);

        return recruiter;
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
            CreatedByRecruiterId = offer.CreatedByRecruiterId
        };
    }
}