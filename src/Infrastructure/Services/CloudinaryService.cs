using System.Net.Http.Json;
using Application.DTOs.Cloudinary.Response;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CloudinaryService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClient =
            httpClientFactory.CreateClient("Cloudinary");

        _configuration = configuration;
    }

    public async Task<CloudinaryUploadResultDto> UploadImageAsync(
        Stream fileStream,
        string fileName)
    {
        using var content =
            new MultipartFormDataContent();

        content.Add(
            new StreamContent(fileStream),
            "file",
            fileName);

        content.Add(
            new StringContent("joblink-preset"),
            "\"upload_preset\"");

        var response =
            await _httpClient.PostAsync(
                "image/upload",
                content);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<CloudinaryResponseDto>();

        return new CloudinaryUploadResultDto
        {
            Url = result!.SecureUrl,
            PublicId = result.PublicId
        };
    }


    public async Task<CloudinaryUploadResultDto> UploadDocumentAsync(
    Stream stream,
    string fileName)
    {
        using var content =
            new MultipartFormDataContent();

        content.Add(
            new StreamContent(stream),
            "file",
            fileName);

        content.Add(
        new StringContent("joblink-preset"),
        "\"upload_preset\"");

        var response =
            await _httpClient.PostAsync(
                "raw/upload",
                content);

        response.EnsureSuccessStatusCode();

        var result =
        await response.Content
            .ReadFromJsonAsync<CloudinaryResponseDto>();

        return new CloudinaryUploadResultDto
        {
            Url = result!.SecureUrl,
            PublicId = result.PublicId
        };
    }
}
