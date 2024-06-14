using FluentValidation;
using InternSystem.Application.Features.InternManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Commands
{

    public class UpdateInternInfoValidator : AbstractValidator<UpdateInternInfoCommand>
    {
        public UpdateInternInfoValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
            //RuleFor(m => m.MSSV).Matches("^(?:$|^SE|^AI|^IA)[0-9]*$"); //Either empty, or a valid FPTU MSSV
            RuleFor(m => m.NgaySinh).LessThan(DateTime.UtcNow.AddDays(7));
            RuleFor(m => m.StartDate).LessThan(m => m.EndDate);
            RuleFor(m => m.EndDate).GreaterThan(m => m.StartDate);
        }
    }

    public class UpdateInternInfoCommand : IRequest<UpdateInternInfoResponse>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
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
        public int? KiThucTapId { get; set; }
        public int? DuAnId { get; set; }


        public string? LastUpdatedBy { get; set; }
    }
}
