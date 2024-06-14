using FluentValidation;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Commands
{
    public class UpdateThongBaoValidator : AbstractValidator<UpdateThongBaoCommand>
    {
        public UpdateThongBaoValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class UpdateThongBaoCommand : IRequest<UpdateThongBaoResponse>
    {
        public int Id { get; set; }

        public string? IdNguoiNhan { get; set; }

        public string? IdNguoiGui { get; set; }

        public string? TieuDe { get; set; }

        public string? NoiDung { get; set; }

        public string? TinhTrang { get; set; }

        public string? LastUpdatedBy { get; set; }

    }
}
