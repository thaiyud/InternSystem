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

    public class ImportUsersValidator : AbstractValidator<ImportCsvCommand>
    {
        public ImportUsersValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .Must(file => file.Length > 0)
                .WithMessage("File is required and cannot be empty.")
                .Must(file => Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Only .csv files are allowed.");
        }
    }

    public class ImportCsvCommand : IRequest<Unit>
    {
        public IFormFile File { get; set; }
    }
}
