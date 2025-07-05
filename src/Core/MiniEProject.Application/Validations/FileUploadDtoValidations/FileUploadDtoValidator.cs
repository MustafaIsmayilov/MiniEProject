using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MiniEProject.Application.DTOs.FileUploadDtos;

namespace MiniEProject.Application.Validations.FileUploadDtoValidations;

public class FileUploadDtoValidator : AbstractValidator<FileUploadDto>
{
    public FileUploadDtoValidator()
    {
        RuleFor(x => x.File)
            .NotEmpty()
            .WithMessage("You have to upload at least 1 file")
            .Must(file => file != null && file.Length <= 5 * 1024 * 1024)
                .WithMessage("File Size can't be higher than 5 MB");
    }
}

