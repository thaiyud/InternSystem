using FluentValidation;
using InternSystem.Application.Features.User.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Commands
{

    public class ImportXlsxValidator : AbstractValidator<ImportXlsxCommand>
    {
        public ImportXlsxValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .Must(file => file.Length > 0)
                .WithMessage("File is required and cannot be empty.")
                .Must(file => Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Only .xlsx files are allowed.");
        }
    }

    public class ImportXlsxCommand : IRequest<UploadFileResponse>
    {
        public IFormFile File { get; set; }
    }
}
