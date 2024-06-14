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
using InternSystem.Application.Features.LichPhongVanManagement.Models;

namespace InternSystem.Application.Features.LichPhongVanManagement.Commands
{
    public class UpdateLichPhongVanValidator : AbstractValidator<UpdateLichPhongVanCommand>
    {
        public UpdateLichPhongVanValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0); // ID phải lớn hơn 0
            RuleFor(m => m.ThoiGianPhongVan)
                .GreaterThan(DateTime.Now)
                .When(m => m.ThoiGianPhongVan.HasValue)
                .WithMessage("Thời gian phỏng vấn phải là một thời điểm trong tương lai.");
        }
    }



    public class UpdateLichPhongVanCommand : IRequest<UpdateLichPhongVanResponse>
    {
        public int Id { get; set; }
        public string? IdNguoiPhongVan { get; set; }
        public int? IdNguoiDuocPhongVan { get; set; }
        public DateTime? ThoiGianPhongVan { get; set; }
        public string? DiaDiemPhongVan { get; set; }
        public bool? DaXacNhanMail { get; set; }
        public string? SendMailResult { get; set; }
        public string? InterviewForm { get; set; }
        public bool? TrangThai { get; set; }
        public string? TimeDuration { get; set; }
        public string? KetQua { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
