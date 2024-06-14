using InternSystem.Application.Features.LichPhongVanManagement.Models;
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
using System.Text.Json.Serialization;

namespace InternSystem.Application.Features.LichPhongVanManagement.Commands
{
    public class CreateLichPhongVanValidator : AbstractValidator<CreateLichPhongVanCommand>
    {
        public CreateLichPhongVanValidator()
        {
            RuleFor(m => m.IdNguoiPhongVan).NotEmpty();
            RuleFor(m => m.IdNguoiDuocPhongVan).NotEmpty();
            RuleFor(m => m.ThoiGianPhongVan).NotEmpty();
            RuleFor(m => m.DiaDiemPhongVan).NotEmpty();
            RuleFor(m => m.ThoiGianPhongVan)
                .GreaterThan(DateTime.Now)
                .WithMessage("Thời gian phỏng vấn phải là một thời điểm trong tương lai.");
        }
    }

    public class CreateLichPhongVanCommand : IRequest<CreateLichPhongVanResponse>
    {
        public string IdNguoiPhongVan { get; set; }
        public string IdNguoiDuocPhongVan { get; set; }
        public DateTime ThoiGianPhongVan { get; set; }
        public string DiaDiemPhongVan { get; set; }
        public bool DaXacNhanMail { get; set; }
        public string? SendMailResult { get; set; }
        public string? InterviewForm { get; set; }
        public bool TrangThai { get; set; }
        public string? TimeDuration { get; set; }
        public string? KetQua { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }

}
