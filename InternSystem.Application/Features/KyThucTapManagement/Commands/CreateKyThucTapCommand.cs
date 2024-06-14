using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using FluentValidation;

namespace InternSystem.Application.Features.KyThucTapManagement.Commands
{

    public class CreateKyThucTapValidator : AbstractValidator<CreateKyThucTapCommand>
    {
        public CreateKyThucTapValidator()
        {
            RuleFor(m => m.Ten).NotEmpty();
            RuleFor(m => m.NgayBatDau).LessThan(m => m.NgayKetThuc);
            RuleFor(m => m.NgayKetThuc).GreaterThan(m => m.NgayBatDau);
            RuleFor(m => m.IdTruong).NotEmpty().GreaterThan(0);
            RuleFor(m => m.CreatedBy).NotEmpty();
        }
    }

    public class CreateKyThucTapCommand : IRequest<CreateKyThucTapResponse>
    {
        public string Ten { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int IdTruong { get; set; }
        public string? CreatedBy { get; set; }
    }
}
