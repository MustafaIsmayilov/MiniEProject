using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MiniEProject.Application.DTOs.FileUploadDtos;

namespace MiniEProject.Application.Validations.FileUploadDtoValidations;

//}
public class FileUploadValidator : AbstractValidator<FileUploadDto>
{
    private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".pdf" };
    private const long _maxFileSize = 5 * 1024 * 1024; // 5 MB

    public FileUploadValidator()
    {
        RuleFor(f => f.File)
            .NotNull().WithMessage("File must be selected.")
            .Must(f => f != null && f.Length > 0)
            .WithMessage("File cannot be empty.")
            .Must(f => f != null && f.Length <= _maxFileSize)
            .WithMessage($"File size must not exceed {_maxFileSize / (1024 * 1024)} MB.")
            .Must(f => f != null && _allowedExtensions
                .Contains(Path.GetExtension(f.FileName).ToLower()))
            .WithMessage("Only .jpg, .jpeg, .png, and .pdf formats are allowed.");
    }
}

