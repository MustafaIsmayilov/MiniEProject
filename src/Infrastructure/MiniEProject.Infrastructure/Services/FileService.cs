using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MiniEProject.Application.Abstracts.Services;

namespace MiniEProject.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }


    public async Task<string> UploadAsync(IFormFile file)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file), "File is null");

        var webRootPath = _env.WebRootPath;
        if (string.IsNullOrEmpty(webRootPath))
            webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        var uploadFolder = Path.Combine(webRootPath, "uploads");
        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{originalFileName}{extension}";

        var filePath = Path.Combine(uploadFolder, fileName);
        int count = 1;

        // Fayl varsa yeni adla davam et
        while (System.IO.File.Exists(filePath))
        {
            fileName = $"{originalFileName}({count}){extension}";
            filePath = Path.Combine(uploadFolder, fileName);
            count++;
        }

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/{fileName}";
    }
}