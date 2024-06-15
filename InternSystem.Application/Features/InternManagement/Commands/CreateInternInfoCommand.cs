using FluentValidation;
using InternSystem.Application.Features.InternManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Commands
{
    public class CreateInternInfoValidator : AbstractValidator<CreateInternInfoCommand>
    {
        public CreateInternInfoValidator()
        {
            RuleFor(m => m.HoTen).NotEmpty().MaximumLength(255);
            RuleFor(m => m.NgaySinh).LessThan(DateTime.UtcNow.AddDays(7));
            RuleFor(m => m.GioiTinh).NotEmpty();
            RuleFor(m => m.EmailTruong).MaximumLength(255);
            RuleFor(m => m.EmailCaNhan).MaximumLength(255);
            RuleFor(m => m.Sdt).MaximumLength(10);
            RuleFor(m => m.SdtNguoiThan).MaximumLength(10);
            RuleFor(m => m.TrangThai).NotEmpty();
            RuleFor(m => m.ViTriMongMuon).MaximumLength(255);
            RuleFor(m => m.StartDate).LessThan(m => m.EndDate);
            RuleFor(m => m.EndDate).GreaterThan(m => m.StartDate);
            RuleFor(m => m.IdTruong).NotEmpty();
        }
    }

    public class CreateInternInfoCommand : IRequest<CreateInternInfoResponse>
    {
        public string? UserId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string MSSV { get; set; }
        public string EmailTruong { get; set; }
        public string EmailCaNhan { get; set; }
        public string Sdt { get; set; }
        public string SdtNguoiThan { get; set; }
        public string DiaChi { get; set; }
        public decimal GPA { get; set; }
        public string TrinhDoTiengAnh { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkCv { get; set; }
        public string NganhHoc { get; set; }
        public string TrangThai { get; set; }
        public int Round { get; set; }
        public string ViTriMongMuon { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int IdTruong { get; set; }
        public int? KyThucTapId { get; set; }
        public int? DuAnId { get; set; }

        public string? CreatedBy { get; set; }

    }
}
