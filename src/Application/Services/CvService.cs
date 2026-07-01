using Application.DTOs.Cv.Request;
using Application.DTOs.Cv.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

public class CvService : ICvService
{
    private readonly ICvRepository _cvRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUserRepository _userRepository;

    public CvService(
        ICvRepository cvRepository,
        ICloudinaryService cloudinaryService,
        IUserRepository userRepository)
    {
        _cvRepository = cvRepository;
        _cloudinaryService = cloudinaryService;
        _userRepository = userRepository;
    }

    public async Task<CvResponseDto> UploadCvAsync(
        int userId,
        Stream stream,
        string fileName)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException($"User", userId);

        if (!user.Status)
            throw new InvalidOperationException("User is not active.");

        var existingCv = await _cvRepository.GetByUserIdAsync(userId);

        var uploadResult = await _cloudinaryService.UploadDocumentAsync(stream, fileName);

        if (existingCv is null)
        {
            var cv = new Cv(
                uploadResult.Url,
                uploadResult.PublicId,
                userId);

            await _cvRepository.AddAsync(cv);

            return new CvResponseDto
            {
                Url = cv.Url,
                PublicId = cv.PublicId,
                UserId = cv.UserId
            };
        }

        existingCv.Update(
            uploadResult.Url,
            uploadResult.PublicId);

        await _cvRepository.UpdateAsync(existingCv);

        return new CvResponseDto
        {
            Id = existingCv.Id,
            Url = existingCv.Url,
            PublicId = existingCv.PublicId,
            UserId = existingCv.UserId
        };
    }

    public async Task<CvResponseDto?> GetByUserIdAsync(int userId)
    {
        var cv = await _cvRepository.GetByUserIdAsync(userId);

        if (cv is null)
            return null;

        return new CvResponseDto
        {
            Id = cv.Id,
            Url = cv.Url,
            PublicId = cv.PublicId,
            UserId = cv.UserId
        };
    }

    public async Task DeleteCvAsync(int userId)
    {
        var cv = await _cvRepository.GetByUserIdAsync(userId);

        if (cv is null)
            throw new NotFoundException($"CV", userId);

        await _cvRepository.DeleteAsync(cv);
    }
}