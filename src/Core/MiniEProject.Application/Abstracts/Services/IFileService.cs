using Microsoft.AspNetCore.Http;

namespace MiniEProject.Application.Abstracts.Services;

public interface IFileService
{
    Task<string> UploadAsync(IFormFile file);
}
