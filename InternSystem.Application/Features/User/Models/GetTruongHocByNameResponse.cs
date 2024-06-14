using System.ComponentModel.DataAnnotations;
using InternSystem.Domain.Entities;

namespace InternSystem.Application.Features.User.Models
{
    public class GetTruongHocByNameResponse : TruongHoc
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int SoTuanThucTap { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
    }
}
