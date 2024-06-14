using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using InternSystem.Application.Features.DuAnManagement.Models;

namespace InternSystem.Application.Features.DuAnManagement.Commands
{
    public class UpdateDuAnValidator : AbstractValidator<UpdateDuAnCommand>
    {
        public UpdateDuAnValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }


    public class UpdateDuAnCommand : IRequest<UpdateDuAnResponse>
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? LeaderId { get; set; }
        public DateTimeOffset? ThoiGianBatDau { get; set; }
        public DateTimeOffset? ThoiGianKetThuc { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
