using Microsoft.AspNetCore.Mvc;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.FileUploadDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniEProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileServices;

        public FilesController(IFileService fileServices)
        {
            _fileServices = fileServices;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] FileUploadDto dto)
        {

            var fileUrl = await _fileServices.UploadAsync(dto.File);
            return Ok(fileUrl);
        }
    }
}
