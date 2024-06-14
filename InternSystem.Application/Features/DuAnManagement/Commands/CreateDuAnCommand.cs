using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace InternSystem.Application.Features.DuAnManagement.Commands
{
    public class CreateDuAnValidator : AbstractValidator<CreateDuAnCommand>
    {
        public CreateDuAnValidator()
        {
            RuleFor(m => m.Ten).NotEmpty();
            RuleFor(m => m.ThoiGianBatDau).LessThan(m => m.ThoiGianKetThuc);
            RuleFor(m => m.ThoiGianKetThuc).GreaterThan(m => m.ThoiGianBatDau);
        }
    }

    public class CreateDuAnCommand : IRequest<CreateDuAnResponse>
    {
        public string Ten { get; set; }
        public string? LeaderId { get; set; }
        public DateTimeOffset ThoiGianBatDau { get; set; }
        public DateTimeOffset ThoiGianKetThuc { get; set; }
        public string? CreatedBy { get; set; }
    }
}
