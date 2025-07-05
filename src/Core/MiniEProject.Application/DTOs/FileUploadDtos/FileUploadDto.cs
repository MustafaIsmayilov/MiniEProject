using Microsoft.AspNetCore.Http;

namespace MiniEProject.Application.DTOs.FileUploadDtos;

public record FileUploadDto
{
    public IFormFile File { get; set; }
}
