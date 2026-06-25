namespace Application.Interfaces;

using Application.DTOs.Cloudinary.Response;

public interface ICloudinaryService
{
    Task<CloudinaryUploadResultDto> UploadImageAsync(
        Stream fileStream,
        string fileName);

    Task<CloudinaryUploadResultDto> UploadDocumentAsync(
        Stream stream,
        string fileName);
}