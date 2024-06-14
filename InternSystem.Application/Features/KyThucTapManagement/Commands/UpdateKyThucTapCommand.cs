using FluentValidation;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.KyThucTapManagement.Commands
{

    public class UpdateKyThucTapValidator : AbstractValidator<UpdateKyThucTapCommand>
    {
        public UpdateKyThucTapValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class UpdateKyThucTapCommand : IRequest<UpdateKyThucTapResponse>
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? IdTruong { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
