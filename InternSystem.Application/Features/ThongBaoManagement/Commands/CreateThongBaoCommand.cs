
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

    public class CreateThongBaoValidator : AbstractValidator<CreateThongBaoCommand>
    {
        public CreateThongBaoValidator()
        {
            RuleFor(m => m.IdNguoiNhan).NotEmpty();
            RuleFor(m => m.IdNguoiGui).NotEmpty();
            RuleFor(m => m.TieuDe).NotEmpty();
            RuleFor(m => m.NoiDung).NotEmpty();
            RuleFor(m => m.TinhTrang).NotEmpty();
        }
    }

    public class CreateThongBaoCommand : IRequest<CreateThongBaoResponse>
    {
        public string IdNguoiNhan { get; set; }

        public string IdNguoiGui { get; set; }

        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string TinhTrang { get; set; }

        public string? CreateBy { get; set; }
    }
}
