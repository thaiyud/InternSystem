using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentValidation;
using InternSystem.Application.Features.InternManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Commands
{
    public class SelfUpdateInternInfoValidator : AbstractValidator<SelfUpdateInternInfoCommand>
    {
        public SelfUpdateInternInfoValidator()
        {
            RuleFor(m => m.NgaySinh).LessThan(DateTime.Now);
        }
    }

    public class SelfUpdateInternInfoCommand : IRequest<UpdateInternInfoResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
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
        public int Round { get; set; }
        public string ViTriMongMuon { get; set; }
        public int IdTruong { get; set; }
        [JsonIgnore]
        public string? LastUpdatedBy { get; set; }
    }
}
