using Application.DTOs.Cv.Response;

namespace Application.Interfaces;

public interface ICvService
{
    Task<CvResponseDto> UploadCvAsync(
        int userId,
        Stream stream,
        string fileName);

    Task<CvResponseDto?> GetByUserIdAsync(
        int userId);

    Task DeleteCvAsync(
        int userId);
}