using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Models
{
    internal class ImportCsvDto
    {
        //public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        //public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        //public string HoVaTen { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public bool IsConfirmed { get; set; }
        public string TrangThaiThucTap { get; set; }
        //public string UserId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string MSSV { get; set; }
        public string EmailTruong { get; set; }
        public string SdtNguoiThan { get; set; }
        public string DiaChi { get; set; }
        public decimal GPA { get; set; }
        public string TrinhDoTiengAnh { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkCv { get; set; }
        public string NganhHoc { get; set; }
        public string Status { get; set; }
        public int Round { get; set; }
        public string ViTriMongMuon { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public int IdTruong { get; set; }
        //public int? KyThucTapId { get; set; }
        //public int? DuAnId { get; set; }
    }
}
